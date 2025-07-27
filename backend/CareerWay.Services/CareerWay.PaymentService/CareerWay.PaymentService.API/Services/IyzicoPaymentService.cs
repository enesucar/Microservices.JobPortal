using CareerWay.PaymentService.API.Interfaces;
using CareerWay.PaymentService.API.Models;
using CareerWay.Shared.Guids;
using CareerWay.Shared.Security.Users;
using CareerWay.Shared.TimeProviders;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;

namespace CareerWay.PaymentService.API.Services;

public class IyzicoPaymentService : IPaymentService
{
    private readonly IyzicoOptions _iyzicoOptions;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTime _dateTime;
    private readonly IPackageRepository _packageRepository;

    public IyzicoPaymentService(IOptions<IyzicoOptions> iyzicoOptions, IGuidGenerator guidGenerator, ICurrentUser currentUser, IDateTime dateTime, IPackageRepository packageRepository)
    {
        _iyzicoOptions = iyzicoOptions.Value;
        _guidGenerator = guidGenerator;
        _currentUser = currentUser;
        _dateTime = dateTime;
        _packageRepository = packageRepository;
    }

    public async Task<PaymentResult> CreatePayment(PaymentRequest request)
    {
        var package = _packageRepository.GetPackageById(request.PackageId);
        if (package == null)
        {
            return PaymentResult.Fail;
        }

        Iyzipay.Options options = new Iyzipay.Options();
        options.ApiKey = _iyzicoOptions.ApiKey;
        options.SecretKey = _iyzicoOptions.SecretKey;
        options.BaseUrl = _iyzicoOptions.BaseUrl;

        CreatePaymentRequest createPaymentRequest = new CreatePaymentRequest();
        createPaymentRequest.Locale = Locale.TR.ToString();
        createPaymentRequest.ConversationId = "123";
        createPaymentRequest.Price = package.Price.ToString();
        createPaymentRequest.PaidPrice = (package.Price * 120 / 100).ToString();
        createPaymentRequest.Currency = Currency.TRY.ToString();
        createPaymentRequest.Installment = 1;
        createPaymentRequest.BasketId = $"B-{createPaymentRequest.ConversationId}";
        createPaymentRequest.PaymentChannel = PaymentChannel.WEB.ToString();
        createPaymentRequest.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = request.CardHolderName;
        paymentCard.CardNumber = request.CardNumber;
        paymentCard.ExpireMonth = request.ExpireMonth;
        paymentCard.ExpireYear = request.ExpireYear;
        paymentCard.Cvc = request.Cvc;
        paymentCard.RegisterCard = 0;
        createPaymentRequest.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "123";
        buyer.Name = request.ContactName;
        buyer.Surname = request.ContactName;
        buyer.GsmNumber = "+905350000000";
        //buyer.Email = _currentUser.Email;
        buyer.IdentityNumber = "11111111111";
        buyer.LastLoginDate = "2013-04-21 15:12:09";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = request.Address;
        buyer.Ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0)?.ToString() ?? "127.0.0.1";
        buyer.City = request.City;
        buyer.Country = request.Country;
        buyer.ZipCode = request.ZipCode;
        buyer.Email = "enes@gmail.com";
        createPaymentRequest.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = request.ContactName;
        shippingAddress.City = request.City;
        shippingAddress.Country = request.Country;
        shippingAddress.Description = request.Address;
        shippingAddress.ZipCode = request.ZipCode;
        createPaymentRequest.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = request.ContactName;
        billingAddress.City = request.City;
        billingAddress.Country = request.Country;
        billingAddress.Description = request.Address;
        billingAddress.ZipCode = request.ZipCode;
        createPaymentRequest.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();
        BasketItem firstBasketItem = new BasketItem();
        firstBasketItem.Id = $"BI-{request.PackageId}";
        firstBasketItem.Name = $"Package-{request.PackageId}";
        firstBasketItem.Category1 = "Package";
        firstBasketItem.Category2 = "Package";
        firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
        firstBasketItem.Price = package.Price.ToString();

        basketItems.Add(firstBasketItem);

        createPaymentRequest.BasketItems = basketItems;

        Payment payment = await Payment.Create(createPaymentRequest, options);
        if (payment.Status == "success")
        {
            return PaymentResult.Success;
        }
        return PaymentResult.Fail;
    }
}
