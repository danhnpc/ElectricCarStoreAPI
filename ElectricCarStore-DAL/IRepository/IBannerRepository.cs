using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.QueryModel;

namespace ElectricCarStore_DAL.IRepository
{
    public interface IBannerRepository
    {
        Task<IEnumerable<BannerViewModel>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
        Task<Banner> AddAsync(Banner banner);
        Task<Banner> UpdateAsync(Banner banner);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
