namespace CareerWay.PaymentService.API.Models;

public class PaymentRequest
{
    public string ContactName { get; set; } = default!;

    public string City { get; set; } = default!;

    public string Country { get; set; } = default!;

    public string Address { get; set; } = default!;

    public string ZipCode { get; set; } = default!;

    public string CardHolderName { get; set; } = default!;

    public string CardNumber { get; set; } = default!;

    public string ExpireMonth { get; set; } = default!;

    public string ExpireYear { get; set; } = default!;

    public string Cvc { get; set; } = default!;

    public int PackageId { get; set; }
}
