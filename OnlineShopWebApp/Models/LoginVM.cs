using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineShopWebApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"(?=.*[0-9])(?=.*[*.!#@$%^&(){}[\]:;<>,.?/~_+\-=|\\])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z*.!#@$%^&(){}[\]:;<>,.?/~_+\-=|\\]{6,}", ErrorMessage = "Пароль должен содержать минимум 6 символов, прописные и строчные буквы, число, и спецсимвол")]
        public string Password { get; set; } = "";

        public bool Remember { get; set; }

        [ValidateNever]
        public string ReturnUrl { get; set; } = "";
    }
}
