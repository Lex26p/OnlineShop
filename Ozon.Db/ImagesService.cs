using Microsoft.AspNetCore.Http;

namespace Ozon.Db
{
    public class ImagesService : IImagesService
    {
        public async Task<List<string>> AddAsync(string folderName, List<IFormFile> images)
        {
            List<string> imagesPath = [];

            if (images.Count > 0)
            {
                
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"../Ozon.Db/ArchiveImages/{folderName}/");

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                foreach (var image in images)
                {
                    string imageName = $"{Guid.NewGuid()}.{image.FileName.Split('.').Last()}";

                    using (var fileStream = new FileStream(imagePath + imageName, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    imagesPath.Add($"/ImagesDb/{folderName}/{imageName}");
                }
            }

            return imagesPath;
        }

        public async Task<string> EditAsync(string folderName, IFormFile image, string oldImagePath)
        {

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"../Ozon.Db/ArchiveImages/{folderName}/");

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            string imageName = $"{Guid.NewGuid()}.{image.FileName.Split('.').Last()}";

            using (var fileStream = new FileStream(imagePath + imageName, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            Delete(oldImagePath);

            return $"/ImagesDb/{folderName}/{imageName}";
        }

        public bool Delete(string path)
        {
            try
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"../Ozon.Db/ArchiveImages/{path}");

                File.Delete(imagePath);

                return true;
            }
            catch { }

            return false;
        }
    }
}