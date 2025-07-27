using CareerWay.Web.Consts;
using CareerWay.Web.Interfaces;
using CareerWay.Web.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerWay.Web.Controllers;

public class PackagesController : Controller
{
    private readonly IPaymentClient _paymentClient;

    public PackagesController(IPaymentClient paymentClient)
    {
        _paymentClient = paymentClient;
    }

    [HttpGet("paket-satin-al")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("paket-satin-al/odeme")]
    public async Task<IActionResult> Payment([FromQuery] int? id)
    {
        var model = new CreatePaymentRequest();
        if (id.HasValue)
        {
            model.PackageId = id.Value;
        };
        return View(model);
    }

    [HttpPost("paket-satin-al/odeme")]
    public async Task<IActionResult> Payment(CreatePaymentRequest request)
    {
        var result = await _paymentClient.Create(request);
        if (result)
        {
            return RedirectToAction("PaymentSuccess");
        }
        return View(request);
    }

    [HttpGet("paket-satin-al/odeme-basarili")]
    public async Task<IActionResult> PaymentSuccess(CreatePaymentRequest request)
    {
        return View();
    }
}
