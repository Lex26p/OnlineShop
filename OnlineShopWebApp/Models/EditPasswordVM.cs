using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineShopWebApp.Models
{
    public class EditPasswordVM
    {
        [ValidateNever]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"(?=.*[0-9])(?=.*[*.!#@$%^&(){}[\]:;<>,.?/~_+\-=|\\])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z*.!#@$%^&(){}[\]:;<>,.?/~_+\-=|\\]{6,}", ErrorMessage = "Пароль должен содержать минимум 6 символов, прописные и строчные буквы, число, и спецсимвол")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RePassword { get; set; } = "";
    }
}