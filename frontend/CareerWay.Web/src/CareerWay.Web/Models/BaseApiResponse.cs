namespace CareerWay.Web.Models;

public class BaseApiResponse<TData, TError>
    where TData : class
    where TError : class
{
    public string Detail { get; set; }

    public TData? Data { get; set; }

    public TError? Errors { get; set; }

    public bool IsSuccess { get; set; }

    public bool IsFail => !IsSuccess;

    public BaseApiResponse()
    {
        Detail = null!;
        Data = null;
        Errors = null;
    }
}

