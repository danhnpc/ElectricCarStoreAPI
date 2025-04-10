using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;

namespace ElectricCarStore_DAL.IRepository
{
    public interface IBannerRepository
    {
        Task<PagedResponse<BannerViewModel>> GetAllAsync(int page = 1, int perPage = 10);
        Task<Banner> GetByIdAsync(int id);
        Task<Banner> AddAsync(Banner banner);
        Task<Banner> UpdateAsync(Banner banner);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
