using Ozon.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class OrderVM(Order order)
    {
        public Guid Id { get; init; } = order.Id;
        public int Num { get; init; } = order.Num;
        public Guid UserId { get; init; } = order.UserId;
        public CartVM Cart { get; set; } = new (order.Cart);
        public string Name { get; set; } = order.Name;
        public string Surname { get; set; } = order.Surname;
        public string Email { get; set; } = order.Email;
        public string Tel { get; set; } = order.Tel;
        public string Address { get; set; } = order.Address;
        public string Comment { get; set; } = order.Comment;
        public DateTime DateCreate { get; set; } = order.DateCreate;
        public OrderStatus Status { get; set; } = order.Status;
    }
}
