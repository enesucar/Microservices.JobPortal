using System.Reflection;
using Castle.DynamicProxy;

namespace CareerWay.Shared.DynamicProxy.CastleWindsor;

public abstract class CastleWindsorMethodInvocationBase : IMethodInvocation
{
    protected IInvocation Invocation { get; }

    protected IInvocationProceedInfo ProceedInfo { get; }

    public object[] Arguments => Invocation.Arguments;

    public Type[] GenericArguments => Invocation.GenericArguments;

    public object InvocationTarget => Invocation.InvocationTarget;

    public MethodInfo Method => Invocation.Method;

    public MethodInfo MethodInvocationTarget => Invocation.MethodInvocationTarget;

    public object ReturnValue { get; set; } = default!;

    public Type TargetType => Invocation.TargetType;

    public CastleWindsorMethodInvocationBase(
        IInvocation invocation,
        IInvocationProceedInfo proceedInfo)
    {
        Invocation = invocation;
        ProceedInfo = proceedInfo;
    }

    public abstract Task ProceedAsync();
}
