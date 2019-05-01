using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education.WEB.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Имя пользователя"), Required]
        public string UserName { get; set; }

        [Display(Name = "Почта"), Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Фамилия"), Required, RegularExpression(@"^[А-ЯЁ][а-яё]{0,16}$")]
        public string Surname { get; set; }

        [Display(Name = "Имя"), Required, RegularExpression(@"^[А-ЯЁ][а-яё]{0,16}$")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Пароль"), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля"), Compare(nameof(Password)), DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
