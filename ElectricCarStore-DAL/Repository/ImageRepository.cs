using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.Model;
namespace ElectricCarStore_DAL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ElectricCarStoreContext _context;

        public ImageRepository(ElectricCarStoreContext context)
        {
            _context = context;
        }

        public async Task<Image> AddAsync(Image entity)
        {
            await _context.Images.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}
