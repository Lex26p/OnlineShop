using System.ComponentModel.DataAnnotations;

namespace Ozon.Db.Models
{
    public class Like
    {
        [Key]
        public Guid UserId { get; init; }
        public List<Product> Products { get; set; } = [];
    }
}