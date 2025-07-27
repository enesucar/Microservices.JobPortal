using Castle.DynamicProxy;

namespace CareerWay.Shared.DynamicProxy.CastleWindsor;

public class CastleWindsorMethodInvocationWithReturnValue<TResult> : CastleWindsorMethodInvocationBase
{
    protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

    public CastleWindsorMethodInvocationWithReturnValue(
        IInvocation invocation,
        IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        : base(invocation, proceedInfo)

    {
        Proceed = proceed;
    }

    public override async Task ProceedAsync()
    {
        ReturnValue = (await Proceed(Invocation, ProceedInfo))!;
    }
}
