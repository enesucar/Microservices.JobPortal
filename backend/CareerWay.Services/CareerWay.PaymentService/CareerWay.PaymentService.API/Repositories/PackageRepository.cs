using CareerWay.PaymentService.API.Interfaces;
using CareerWay.PaymentService.API.Models;

namespace CareerWay.PaymentService.API.Repositories;

public class PackageRepository : IPackageRepository
{
    private static List<Package> _packages = [];

    public PackageRepository()
    {
        _packages = new List<Package>()
        {
            new Package()
            {
                Id = 1,
                Price = 30m
            },
            new Package()
            {
                Id = 2,
                Price = 50m
            },
            new Package()
            {
                Id = 3,
                Price = 75m
            }
        };
    }

    public Package GetPackageById(int id)
    {
        return _packages.FirstOrDefault(o => o.Id == id)!;
    }
}
