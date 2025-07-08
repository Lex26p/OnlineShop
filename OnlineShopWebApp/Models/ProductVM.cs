using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductVM
    {
        [ValidateNever]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Укажите название товара")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Название должно содержать от 1 до 40 символов")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Укажите цену товара")]
        [RegularExpression(@"^(?!0+[1-9])\d{1,9}(?:[\.,]\d{1,2})?$", ErrorMessage = "Ошибка данных. ведите число с разделителем дробной части - точка. после точки может быть только 2 цифры")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "Укажите описание товара")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Описание должно содержать от 5 до 200 символов")]
        public string? Description { get; set; } = "";

        [ValidateNever]
        public List<string> ImagesPath { get; set; } = [];
        public List<IFormFile> FormImage { get; set; } = [];
        public ProductVM() { }

        public ProductVM(Guid id, string name, decimal cost, string description)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
        }
    }
}
