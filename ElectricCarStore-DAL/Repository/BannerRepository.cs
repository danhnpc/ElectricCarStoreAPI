using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly ElectricCarStoreContext _context;

        public BannerRepository(ElectricCarStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Banner>> GetAllAsync()
        {
            return await _context.Banners
                .Where(b => b.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Banner> GetByIdAsync(int id)
        {
            return await _context.Banners
                .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted != true);
        }

        public async Task<Banner> AddAsync(Banner banner)
        {
            banner.IsDeleted = false;
            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();
            return banner;
        }

        public async Task<Banner> UpdateAsync(Banner banner)
        {
            _context.Entry(banner).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return banner;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
                return false;

            // Soft delete
            banner.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Banners
                .AnyAsync(b => b.Id == id && b.IsDeleted != true);
        }
    }

}
