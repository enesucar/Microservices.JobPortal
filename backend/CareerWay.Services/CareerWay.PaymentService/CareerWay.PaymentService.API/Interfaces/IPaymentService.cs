using CareerWay.PaymentService.API.Models;

namespace CareerWay.PaymentService.API.Interfaces;

public interface IPaymentService
{
    Task<PaymentResult> CreatePayment(PaymentRequest request);
}
