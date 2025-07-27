using CareerWay.Shared.Validation.Validation;
using FluentValidation;
using MediatR;

namespace CareerWay.Shared.MediatR.Behaviours.Validation;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

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

        return await next();
    }
}
