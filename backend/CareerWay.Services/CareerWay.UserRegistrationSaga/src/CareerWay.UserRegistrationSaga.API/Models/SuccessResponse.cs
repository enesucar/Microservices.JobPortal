using CareerWay.Shared.AspNetCore.Models;

namespace CareerWay.UserRegistrationSaga.API.Models;

public class SuccessResponse<TData> : BaseApiResponse
{
    public TData? Data { get; set; }
}
