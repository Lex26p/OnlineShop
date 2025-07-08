using Ozon.Db.Models;

namespace Ozon.Db
{
    public interface ICartsStorage
    {
        Task<bool> AddAsync(Guid userId, Product product);
        Task<bool> ClearAsync(Guid userId);
        Task<Cart?> GetAsync(Guid userId);
        Task<bool> ReduceAsync(Guid userId, Guid productId);
    }
}