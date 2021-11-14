using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schedule.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "ФИО")]
        public string FIO { get; set; }
        [Required]
        [Display(Name = "Учебное заведение")]
        public string EducationalInstitutions { get; set; }
        [Required]
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }
        [Required]
        [Display(Name = "Логин")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

    }
}
