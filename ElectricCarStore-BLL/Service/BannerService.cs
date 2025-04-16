using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.Service
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<PagedResponse<BannerViewModel>> GetAllBannersAsync(int page, int perPage)
        {
            return await _bannerRepository.GetAllAsync(page, perPage);
        }

        public async Task<BannerViewModel> GetBannerByIdAsync(int id)
        {
            var banner = await _bannerRepository.GetDetailsByIdAsync(id);
            if (banner == null)
                throw new KeyNotFoundException($"Banner with ID {id} not found.");

            return banner;
        }

        public async Task<Banner> CreateBannerAsync(BannerPostModel bannerModel)
        {
            var banner = new Banner
            {
                Title = bannerModel.Title,
                Description = bannerModel.Description,
                ImageId = bannerModel.ImageId,
                IsDeleted = false
            };

            return await _bannerRepository.AddAsync(banner);
        }

        public async Task<Banner> UpdateBannerAsync(int id, BannerPostModel bannerModel)
        {
            var existingBanner = await _bannerRepository.GetByIdAsync(id);
            if (existingBanner == null)
                throw new KeyNotFoundException($"Banner with ID {id} not found.");

            // Cập nhật các thuộc tính từ model
            // Cập nhật các thuộc tính từ model
            existingBanner.Title = bannerModel.Title;
            existingBanner.Description = bannerModel.Description;
            existingBanner.ImageId = bannerModel.ImageId;

            return await _bannerRepository.UpdateAsync(existingBanner);
        }

        public async Task<bool> DeleteBannerAsync(int id)
        {
            var exists = await _bannerRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Banner with ID {id} not found.");

            return await _bannerRepository.DeleteAsync(id);
        }
    }

}
