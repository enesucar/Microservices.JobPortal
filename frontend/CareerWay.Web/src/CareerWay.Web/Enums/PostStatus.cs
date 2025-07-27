using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum PostStatus : byte
{
    [Display(Name = "Hazırlanıyor")]
    Draft = 0,
    [Display(Name = "Bekleniliyor")]
    Pending,
    [Display(Name = "Yayınlandı")]
    Approved = 3,
    [Display(Name = "Reddedildi")]
    Rejected
}
