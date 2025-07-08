using Ozon.Db.Models;

namespace Ozon.Db
{
    public interface IProductsStorage
    {
        Task<Product?> GetAsync(Guid id);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> SearchAsync(string text);
        Task<bool> AddAsync(Product product);
        Task<bool> EditAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> EditImagesAsync(Guid id, List<string> images);
        Task<bool> DeleteImageAsync(Guid id, string path);
    }
}