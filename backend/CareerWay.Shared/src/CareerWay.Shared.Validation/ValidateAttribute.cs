using CareerWay.Shared.DynamicProxy;
using System.Xml;

namespace CareerWay.Shared.Validation.FluentValidation;

public class ValidateAttribute : BaseInterceptionAttribute
{
    public Type ValidatorType { get; set; }

    public ValidateAttribute(Type validatorType)
    {
        ValidatorType = validatorType;
    }
}
