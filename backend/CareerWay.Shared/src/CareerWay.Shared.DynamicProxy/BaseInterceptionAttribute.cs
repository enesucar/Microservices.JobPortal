namespace CareerWay.Shared.DynamicProxy;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class BaseInterceptionAttribute : Attribute
{
}
