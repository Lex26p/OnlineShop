using System.ComponentModel.DataAnnotations;

namespace Ozon.Db.Models
{
    public class Compare
    {
        [Key]
        public Guid UserId { get; init; }
        public List<Product> Products { get; set; } = [];
    }
}