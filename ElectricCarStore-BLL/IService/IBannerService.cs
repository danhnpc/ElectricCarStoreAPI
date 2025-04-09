using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface IBannerService
    {
        Task<IEnumerable<BannerViewModel>> GetAllBannersAsync();
        Task<Banner> GetBannerByIdAsync(int id);
        Task<Banner> CreateBannerAsync(BannerPostModel bannerModel);
        Task<Banner> UpdateBannerAsync(int id, BannerPostModel bannerModel);
        Task<bool> DeleteBannerAsync(int id);
    }

}
