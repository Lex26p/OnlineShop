namespace Ozon.Db.Models
{
    public class Cart
    {
        public Guid Id { get; init; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = [];
        public List<Order> Orders { get; set; } = [];
    }
}