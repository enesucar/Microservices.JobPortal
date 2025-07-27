using CareerWay.PaymentService.API.Models;

namespace CareerWay.PaymentService.API.Interfaces;

public interface IPackageRepository
{
    Package GetPackageById(int id);
}
