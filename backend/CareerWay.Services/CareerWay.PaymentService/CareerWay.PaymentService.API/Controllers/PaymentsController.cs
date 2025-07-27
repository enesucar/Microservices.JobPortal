using Asp.Versioning;
using CareerWay.PaymentService.API.IntegrationEvents.Events;
using CareerWay.PaymentService.API.Interfaces;
using CareerWay.PaymentService.API.Models;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.Security.Users;
using CareerWay.Shared.TimeProviders;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.PaymentService.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ICorrelationId _correlationId;
    private readonly IUser _currentUser;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;

    public PaymentsController(
        IPaymentService paymentService,
        ICorrelationId correlationId,
        IUser currentUser,
        IEventBus eventBus,
        IDateTime dateTime)
    {
        _paymentService = paymentService;
        _correlationId = correlationId;
        _currentUser = currentUser;
        _eventBus = eventBus;
        _dateTime = dateTime;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment(PaymentRequest request)
    {
        var result = await _paymentService.CreatePayment(request);
        if (result == PaymentResult.Success)
        {
            await _eventBus.PublishAsync(new PaymentSuccessIntegrationEvent(_correlationId.Get(), _dateTime.Now)
            {
                CompanyId = _currentUser.Id!,
                PackageId = request.PackageId,
                CreationDate = DateTime.Now,
            });
            return Created("", result);
        }
        return BadRequest(result);
    }
}
