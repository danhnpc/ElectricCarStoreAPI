using CloudinaryDotNet.Actions;
using ElectricCarStore_DAL.Models.Model;
using Microsoft.AspNetCore.Http;

namespace ElectricCarStore_BLL.IService
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<Image> SaveImageAsync(string url);
        public Task<bool> DeleteImageAsync(int id);
    }
}
