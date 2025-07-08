namespace Ozon.Db.Models
{
    public class CartItem
    {
        public Guid Id { get; init; }
        public Product Product { get; set; } = new();
        public Cart Cart { get; set; } = new();
        public int Count { get; set; }
    }
}
