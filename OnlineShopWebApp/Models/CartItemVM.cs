using Ozon.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class CartItemVM(CartItem item)
    {
        public Product Product { get; init; } = item.Product;
        public int Count { get; set; } = item.Count;
        public decimal Cost
        {
            get
            {
                return Product.Cost * Count;
            }
        }
    }
}
