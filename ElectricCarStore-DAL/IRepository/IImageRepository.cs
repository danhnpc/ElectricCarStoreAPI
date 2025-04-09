using ElectricCarStore_DAL.Models.Model;

namespace ElectricCarStore_DAL.IRepository
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image> GetByIdAsync(int id);
        Task<Image> AddAsync(Image entity);
        Task<bool> UpdateAsync(Image entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DisableAsync(int id);
    }
}
