using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class EditUserVM
    {
        [ValidateNever]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Имя должно содержать от 1 до 40 символов")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Фамилия должна содержать от 1 до 40 символов")]
        public string Surname { get; set; } = "";

        [Required(ErrorMessage = "Не указан телефон")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Не указан адрес доставки")]
        public string Address { get; set; } = "";

        public EditUserVM()
        {

        }

        public EditUserVM(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            PhoneNumber = user.PhoneNumber ?? "";
            Address = user.Address;
        }
    }
}