using System.Reflection;

namespace CareerWay.Shared.DynamicProxy;

public class LogInterceptor : MethodInterceptionBase
{
    protected override async Task OnEntryAsync(IMethodInvocation methodInvocation)
    {
        await Console.Out.WriteAsync($"[LOG] Before executing {methodInvocation.MethodInvocationTarget.Name}{Environment.NewLine}");
    }

    protected override async Task OnSuccessAsync(IMethodInvocation methodInvocation)
    {
        await Console.Out.WriteAsync($"[LOG] After executing {methodInvocation.MethodInvocationTarget.Name}{Environment.NewLine}");
    }

    protected override async Task<bool> ShouldInterceptAsync(IMethodInvocation methodInvocation)
    {
        return await Task.FromResult(
            methodInvocation.MethodInvocationTarget.GetCustomAttributes<LogAttribute>(true).Any());
    }
}
