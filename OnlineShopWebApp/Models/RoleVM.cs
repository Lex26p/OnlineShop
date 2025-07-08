using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Ozon.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class RoleVM
    {
        [ValidateNever]
        public Guid Id { get; init; } = Guid.NewGuid();
        [Required(ErrorMessage = "Укажите название роли")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Название должно содержать от 4 до 15 символов")]
        [RegularExpression(@"^[a-zA-Z](.[a-zA-Z0-9_-]*)$", ErrorMessage = "Название должно содержать только латинские буквы, цифры и символы - _")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Укажите описание товара")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Описание должно содержать от 5 до 200 символов")]
        public string Description { get; set; } = "";

        public RoleVM(){ }
        public RoleVM(Role role)
        {
            Id = role.Id;
            Name = role.Name ?? "";
            Description = role.Description;
        }
    }
}