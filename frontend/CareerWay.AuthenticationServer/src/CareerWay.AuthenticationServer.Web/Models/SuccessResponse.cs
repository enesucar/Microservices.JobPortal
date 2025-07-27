using CareerWay.Shared.AspNetCore.Models;

namespace CareerWay.AuthenticationServer.Web.Models;

public class SuccessResponse<TData> : BaseApiResponse
{
    public TData? Data { get; set; }
}
