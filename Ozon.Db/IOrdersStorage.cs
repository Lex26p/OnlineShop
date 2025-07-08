using Ozon.Db.Models;

namespace Ozon.Db
{
    public interface IOrdersStorage
    {
        Task<bool> AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetAsync(Guid id);
        Task<bool> ChangeStatusAsync(Guid id, OrderStatus status);
        Task<List<Order>> UserGetAllAsync(Guid id);
    }
}