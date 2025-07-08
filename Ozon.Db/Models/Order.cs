using System.ComponentModel.DataAnnotations.Schema;

namespace Ozon.Db.Models
{
    public class Order
    {
        public Guid Id { get; init; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Num { get; init; }
        public Guid UserId { get; set; }
        public Cart Cart { get; set; } = new();
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Tel { get; set; } = "";
        public string Address { get; set; } = "";
        public string Comment { get; set; } = "";
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Created;
    }
}
