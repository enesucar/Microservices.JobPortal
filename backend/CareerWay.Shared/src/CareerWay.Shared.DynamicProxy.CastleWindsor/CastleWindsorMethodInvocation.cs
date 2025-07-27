using Castle.DynamicProxy;

namespace CareerWay.Shared.DynamicProxy.CastleWindsor;

public class CastleWindsorMethodInvocation : CastleWindsorMethodInvocationBase
{
    protected Func<IInvocation, IInvocationProceedInfo, Task> Proceed { get; }

    public CastleWindsorMethodInvocation(
        IInvocation invocation,
        IInvocationProceedInfo proceedInfo,
        Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        : base(invocation, proceedInfo)
    {
        Proceed = proceed;
    }

    public override async Task ProceedAsync()
    {
        await Proceed(Invocation, ProceedInfo);
    }
}
