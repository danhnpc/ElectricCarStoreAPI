using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.Model;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
                return false;

            // Soft delete
            image.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisableAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                throw new KeyNotFoundException($"Image with ID {id} not found.");
            }
            // Soft delete
            image.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Images
               .AnyAsync(b => b.Id == id && b.IsDeleted != true);
        }

        public Task<IEnumerable<Image>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Image entity)
        {
            throw new NotImplementedException();
        }

    }
}
