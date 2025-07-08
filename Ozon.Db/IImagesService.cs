using Microsoft.AspNetCore.Http;

namespace Ozon.Db
{
    public interface IImagesService
    {
        Task<List<string>> AddAsync(string folderName, List<IFormFile> images);
        bool Delete(string path);
        Task<string> EditAsync(string folderName, IFormFile image, string oldImagePath);
    }
}