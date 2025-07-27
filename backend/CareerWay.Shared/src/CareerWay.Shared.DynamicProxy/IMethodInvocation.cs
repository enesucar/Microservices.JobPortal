using System.Reflection;

namespace CareerWay.Shared.DynamicProxy;

public interface IMethodInvocation
{
    object[] Arguments { get; }

    Type[] GenericArguments { get; }

    object InvocationTarget { get; }

    MethodInfo Method { get; }

    MethodInfo MethodInvocationTarget { get; }

    object ReturnValue { get; set; }

    Type TargetType { get; }

    Task ProceedAsync();
}
