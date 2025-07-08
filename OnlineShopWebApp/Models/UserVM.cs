using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserVM
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Имя должно содержать от 1 до 40 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Фамилия должна содержать от 1 до 40 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона")]
        public string Tel { get; set; }
        public string Password { get; set; }

        public UserVM(string email, string password, string name, string surname, string tel)
        {
            Id = new Guid();
            Email = email.Trim();
            Password = password;
            Name = name;
            Surname = surname;
            Tel = tel;
        }
    }
}
