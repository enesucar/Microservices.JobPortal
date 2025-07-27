using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum WorkingType : byte
{
    [Display(Name = "Tam Zamanlı")]
    FullTime = 1,

    [Display(Name = "Sözleşmeli / Proje Bazlı")]
    ContractOrProjectBased,

    [Display(Name = "Yarı Zamanlı")]
    PartTime,

    [Display(Name = "Esnek Zamanlı")]
    FlexibleTime
}
