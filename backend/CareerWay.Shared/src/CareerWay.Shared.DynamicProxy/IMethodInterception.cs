namespace CareerWay.Shared.DynamicProxy;

public interface IMethodInterception
{
    Task InterceptAsync(IMethodInvocation methodInvocation);
}
