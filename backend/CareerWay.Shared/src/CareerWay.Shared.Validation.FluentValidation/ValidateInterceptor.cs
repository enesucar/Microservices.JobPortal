using CareerWay.Shared.DynamicProxy;
using CareerWay.Shared.Validation.Validation;
using FluentValidation;
using System.Reflection;

namespace CareerWay.Shared.Validation.FluentValidation;

public class ValidateInterceptor : MethodInterceptionBase
{
    private readonly IServiceProvider _serviceProvider;

    public ValidateInterceptor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected async override Task OnEntryAsync(IMethodInvocation methodInvocation)
    {
        var validateAttribute = methodInvocation.MethodInvocationTarget.GetCustomAttributes<ValidateAttribute>(true).FirstOrDefault();
        if (validateAttribute == null)
        {
            await methodInvocation.ProceedAsync();
            return;
        }

        if (!typeof(IValidator).IsAssignableFrom(validateAttribute.ValidatorType))
        {
            throw new ArgumentException("Argument must implement IValidator.");
        }

        var argumentsToValidate = methodInvocation
            .Arguments
            .Where(argument => argument.GetType() == validateAttribute.ValidatorType)
            .ToList();

        if (!argumentsToValidate.Any())
        {
            await methodInvocation.ProceedAsync();
            return;
        }

        var validatorType = typeof(IValidator<>).MakeGenericType(validateAttribute.ValidatorType);
        var validator = _serviceProvider.GetService(validatorType) as IValidator;
        if (validator == null)
        {
            return;
        }

        var validationResults = await Task.WhenAll(argumentsToValidate.Select(argumentToValidate =>
        {
            var context = new ValidationContext<object>(argumentToValidate);
            return validator.ValidateAsync(context);
        }));

        var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(o => o.Errors).ToList();
        if (failures.Any())
        {
            var errors = failures.GroupBy(o => o.PropertyName).Select(o => new ValidationFailureResult()
            {
                PropertyName = o.Key,
                ErrorMessages = [.. o.Select(j => j.ErrorMessage)]
            }).ToList();

            throw new ValidationFailureException(errors: errors);
        }
    }
}
