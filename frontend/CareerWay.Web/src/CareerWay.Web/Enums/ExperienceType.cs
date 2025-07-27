using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum ExperienceType : byte
{
    [Display(Name = "Tecrübeli/Tecrübesiz")]
    Both = 1,
    [Display(Name = "Tecrübeli")]
    Experienced,
    [Display(Name = "Tecrübesiz")]
    Inexperienced,
}
