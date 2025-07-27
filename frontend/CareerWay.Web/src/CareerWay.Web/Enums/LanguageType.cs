using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum LanguageType : byte
{
    [Display(Name = "Türkçe")]
    Turkish = 1,
    [Display(Name = "İngilizce")]
    English = 2,
    [Display(Name = "Almanca")]
    German = 3,
    [Display(Name = "Fransızca")]
    French = 4,
    [Display(Name = "İspanyolca")]
    Spanish = 5,
    [Display(Name = "İtalyanca")]
    Italian = 6,
    [Display(Name = "Rusça")]
    Russian = 7,
    [Display(Name = "Arapça")]
    Arabic = 8,
    [Display(Name = "Çince")]
    Chinese = 9,
    [Display(Name = "Japonca")]
    Japanese = 10,
    [Display(Name = "Korece")]
    Korean = 11,
    [Display(Name = "Portekizce")]
    Portuguese = 12,
    [Display(Name = "Hintçe")]
    Hindi = 13,
    [Display(Name = "Flemenkçe")]
    Dutch = 14,
    [Display(Name = "Lehçe")]
    Polish = 15
}
