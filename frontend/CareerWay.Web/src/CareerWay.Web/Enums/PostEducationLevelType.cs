using System.ComponentModel.DataAnnotations;

namespace Web.Domain.Enums;

public enum PostEducationLevelType : byte
{
    [Display(Name = "Doktora Mezunu")]
    DoctorateGraduate = 1,

    [Display(Name = "Doktora Öğrencisi")]
    DoctorateStudent,

    [Display(Name = "Yüksek Lisans Mezunu")]
    MastersGraduate,

    [Display(Name = "Yüksek Lisans Öğrencisi")]
    MastersStudent,

    [Display(Name = "Lisans Mezunu")]
    BachelorsGraduate,

    [Display(Name = "Lisans Öğrencisi")]
    BachelorsStudent,

    [Display(Name = "Ön Lisans Mezunu")]
    AssociateGraduate,

    [Display(Name = "Ön Lisans Öğrencisi")]
    AssociateStudent,

    [Display(Name = "Lise Mezunu")]
    HighSchoolGraduate,

    [Display(Name = "Lise Öğrencisi")]
    HighSchoolStudent,

    [Display(Name = "İlkokul Mezunu")]
    PrimarySchoolGraduate,

    [Display(Name = "İlkokul Öğrencisi")]
    PrimarySchoolStudent
}
