using Castle.DynamicProxy;

namespace CareerWay.Shared.DynamicProxy.CastleWindsor;

public class AsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
     where TInterceptor : IMethodInterception
{
    public AsyncDeterminationInterceptor(TInterceptor asyncInterceptor)
        : base(new CastleWindsorMethodInterception<TInterceptor>(asyncInterceptor))
    {
    }
}

