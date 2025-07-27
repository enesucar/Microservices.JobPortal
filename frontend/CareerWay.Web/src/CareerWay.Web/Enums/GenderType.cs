using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum GenderType : byte
{
    [Display(Name = "Erkek/Kadın")]
    Both = 1,
    [Display(Name = "Erkek")]
    Male,
    [Display(Name = "Kadın")]
    Female
}
