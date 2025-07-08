using Ozon.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class CartVM
    {
        public Guid Id { get; init; }
        public Guid UserId { get; set; }
        public List<CartItemVM> Items { get; set; } = [];
        public decimal Cost
        {
            get
            {
                return Items.Sum(i => i.Cost);
            }
        }

        public int Amount
        {
            get
            {
                return Items.Sum(i => i.Count);
            }
        }

        public CartVM(Cart cart)
        {
            Id = cart.Id;
            UserId = cart.UserId;

            foreach (var p in cart.Items)
            {
                Items.Add(new CartItemVM(p));
            }
        }
    }
}