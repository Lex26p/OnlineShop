using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class OrderInputVM
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
        public string Tel { get; set; } = "";

        [Required(ErrorMessage = "Не указан адрес доставки")]
        public string Address { get; set; } = "";
        [Required(ErrorMessage = "Укажите описание")]
        public string Comment { get; set; } = "";
    }
}
