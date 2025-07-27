using CareerWay.Web.Models.Payment;

namespace CareerWay.Web.Interfaces;

public interface IPaymentClient
{
    Task<bool> Create(CreatePaymentRequest request);
}
