namespace CareerWay.Shared.DynamicProxy;

public abstract class MethodInterceptionBase : IMethodInterception
{
    public virtual async Task InterceptAsync(IMethodInvocation methodInvocation)
    {
        if (!await ShouldInterceptAsync(methodInvocation))
        {
            await methodInvocation.ProceedAsync();
            return;
        }

        await OnEntryAsync(methodInvocation);

        try
        {
            await methodInvocation.ProceedAsync();
        }
        catch (Exception e)
        {
            await OnExceptionAsync(methodInvocation, e);
            throw;
        }
        finally
        {
            await OnExitAsync(methodInvocation);
        }

        await OnSuccessAsync(methodInvocation);
    }

    protected virtual async Task<bool> ShouldInterceptAsync(IMethodInvocation methodInvocation)
    {
        return await Task.FromResult(true);
    }

    protected virtual async Task OnEntryAsync(IMethodInvocation methodInvocation)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnExceptionAsync(IMethodInvocation methodInvocation, Exception exception)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnExitAsync(IMethodInvocation methodInvocation)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnSuccessAsync(IMethodInvocation methodInvocation)
    {
        await Task.CompletedTask;
    }
}
