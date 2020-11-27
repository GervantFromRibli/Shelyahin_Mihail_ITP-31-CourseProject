using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Роль пользователя")]
        public string UserRole { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "ФИО пользователя")]
        public string FIO { get; set; }

        [Required]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
