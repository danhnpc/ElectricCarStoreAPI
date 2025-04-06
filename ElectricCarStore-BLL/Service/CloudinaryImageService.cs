using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ElectricCarStore_BLL.IService;
using Microsoft.AspNetCore.Http;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models.Model;

namespace ElectricCarStore_BLL.Service
{
    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        private readonly IImageRepository _imageRepository;

        public CloudinaryImageService(
            Cloudinary cloudinary,
            IImageRepository imageRepository
            )
        {
            _cloudinary = cloudinary;
            _imageRepository = imageRepository;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded");

            using var stream = file.OpenReadStream();

            // Tạo tên file ngẫu nhiên để tránh trùng lặp
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(Guid.NewGuid().ToString(), stream),
                Transformation = new Transformation().Width(1000).Height(1000).Crop("limit"),
                Folder = "ElectricCarImage" // Thay đổi thành folder bạn muốn lưu trữ
            };

            return await _cloudinary.UploadAsync(uploadParams);
        }

        public async Task<Image> SaveImageAsync(string url)
        {
            var result = await _imageRepository.AddAsync(new Image
            {
                Url = url,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            });
            return result;
        }
    }
}
