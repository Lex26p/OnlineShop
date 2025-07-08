using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineShopWebApp.Models
{
    public class NewUserVM
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Имя должно содержать от 1 до 40 символов")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Фамилия должна содержать от 1 до 40 символов")]
        public string Surname { get; set; } = "";

        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Не указан телефон")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Не указан адрес доставки")]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"(?=.*[0-9])(?=.*[*.!#@$%^&(){}[\]:;<>,.?/~_+\-=|\\])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z*.!#@$%^&(){}[\]:;<>,.?/~_+\-=|\\]{6,}", ErrorMessage = "Пароль должен содержать минимум 6 символов, прописные и строчные буквы, число, и спецсимвол")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RePassword { get; set; } = "";

        [ValidateNever]
        public string ReturnUrl { get; set; } = "";

        [ValidateNever]
        public List<string> ImagesPath { get; set; } = [];

        [ValidateNever]
        public IFormFile? FormImage { get; set; }
    }
}