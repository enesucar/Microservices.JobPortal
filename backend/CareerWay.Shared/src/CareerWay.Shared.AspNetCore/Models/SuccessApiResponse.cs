namespace CareerWay.Shared.AspNetCore.Models;

public class SuccessApiResponse : BaseApiResponse
{
    public SuccessApiResponse()
    {
        Status = 200;
    }
}

public class SuccessApiResponse<TData> : SuccessApiResponse
{
    public TData? Data { get; set; } = default!;

    public SuccessApiResponse(TData data)
    { 
    }
}
