using ElectricCarStore_DAL.Models.Model;

namespace ElectricCarStore_DAL.IRepository
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
        Task<Banner> AddAsync(Banner banner);
        Task<Banner> UpdateAsync(Banner banner);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
