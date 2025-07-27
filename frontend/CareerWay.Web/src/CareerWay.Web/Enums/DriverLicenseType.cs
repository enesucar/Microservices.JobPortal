using System.ComponentModel.DataAnnotations;

namespace CareerWay.Web.Enums;

public enum DriverLicenseType : byte
{
    [Display(Name = "Lisanslı/Lisanssız")]
    NoLicense = 1,
    [Display(Name = "A1")]
    A1,
    [Display(Name = "A2")]
    A2,
    [Display(Name = "A")]
    A,
    [Display(Name = "B1")]
    B1,
    [Display(Name = "B")]
    B,
    [Display(Name = "BE")]
    BE,
    [Display(Name = "C1")]
    C1,
    [Display(Name = "C1E")]
    C1E,
    [Display(Name = "C")]
    C,
    [Display(Name = "CE")]
    CE,
    [Display(Name = "D1")]
    D1,
    [Display(Name = "D1E")]
    D1E,
    [Display(Name = "D")]
    D,
    [Display(Name = "DE")]
    DE,
    [Display(Name = "F")]
    F,
    [Display(Name = "M")]
    M
}
