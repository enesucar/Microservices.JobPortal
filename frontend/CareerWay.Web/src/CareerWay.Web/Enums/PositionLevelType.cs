using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum PositionLevelType : byte
{
    [Display(Name = "Üst Düzey Yönetici")]
    BestExecutive = 0,

    [Display(Name = "Üst Düzey Yönetici")]
    TopExecutive = 1,

    [Display(Name = "Orta Düzey Yönetici")]
    MidLevelManager,

    [Display(Name = "Yönetici Adayı")]
    ManagerCandidate,

    [Display(Name = "Uzman")]
    Specialist,

    [Display(Name = "Yeni Mezun / Giriş Seviyesi")]
    EntryLevel,

    [Display(Name = "Freelance")]
    Freelancer,

    [Display(Name = "Mavi Yaka")]
    BlueCollarWorker,

    [Display(Name = "Stajyer / Asistan / Uzman Yardımcısı")]
    InternAssistantSpecialist,

    [Display(Name = "Personel / Memur")]
    StaffMember,

    [Display(Name = "Hizmetli / Destek Personeli")]
    ServicePersonnel
}
