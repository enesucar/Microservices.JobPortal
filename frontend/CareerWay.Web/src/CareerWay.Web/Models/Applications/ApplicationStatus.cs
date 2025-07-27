using System.ComponentModel.DataAnnotations;

namespace CareerWay.Web.Models.Applications;

public enum ApplicationStatus : byte
{
    [Display(Name = "Başvuruldu")]
    Pending,
    [Display(Name = "Kabul Edildi")]
    Approved,
    [Display(Name = "Reddedildi")]
    Rejected
}
