using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;

namespace ElectricCarStore_BLL.IService
{
    public interface IBannerService
    {
        Task<PagedResponse<BannerViewModel>> GetAllBannersAsync(int page, int perPage);
        Task<Banner> GetBannerByIdAsync(int id);
        Task<Banner> CreateBannerAsync(BannerPostModel bannerModel);
        Task<Banner> UpdateBannerAsync(int id, BannerPostModel bannerModel);
        Task<bool> DeleteBannerAsync(int id);
    }

}
