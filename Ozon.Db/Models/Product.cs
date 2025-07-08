
using System.ComponentModel.DataAnnotations.Schema;

namespace Ozon.Db.Models
{
    public class Product
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = "";
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public string Description { get; set; } = "";
        public List<string> ImagesPath { get; set; } = [];
        public List<CartItem> Items { get; set; } = [];
        public List<Compare> Compares { get; set; } = [];
    }
}
