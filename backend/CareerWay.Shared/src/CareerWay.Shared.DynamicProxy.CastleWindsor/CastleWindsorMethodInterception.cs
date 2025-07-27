using Castle.DynamicProxy;

namespace CareerWay.Shared.DynamicProxy.CastleWindsor;

public class CastleWindsorMethodInterception<TInterceptor> : AsyncInterceptorBase
     where TInterceptor : IMethodInterception
{
    private readonly TInterceptor _interceptor;

    public CastleWindsorMethodInterception(TInterceptor interceptor)
    {
        _interceptor = interceptor;
    }

    protected override async Task InterceptAsync(
        IInvocation invocation,
        IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task> proceed)
    {
        await _interceptor.InterceptAsync(new CastleWindsorMethodInvocation(
           invocation,
           proceedInfo,
           proceed));
    }

    protected override async Task<TResult> InterceptAsync<TResult>(
        IInvocation invocation,
        IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
    {
        var methodInvocation = new CastleWindsorMethodInvocationWithReturnValue<TResult>(
            invocation,
            proceedInfo,
            proceed);
        await _interceptor.InterceptAsync(methodInvocation);
        return (TResult)methodInvocation.ReturnValue;
    }
}
