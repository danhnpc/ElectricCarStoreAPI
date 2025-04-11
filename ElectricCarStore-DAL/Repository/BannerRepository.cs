using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;
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

        public async Task<PagedResponse<BannerViewModel>> GetAllAsync(int page = 1, int perPage = 10)
        {
            var query = _context.Banners.Where(b => b.IsDeleted != true);
            int totalRecords = await query.CountAsync();

            var banners = await query
                .OrderByDescending(b => b.Id)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .Select(b => new BannerViewModel
                {
                    Id = b.Id,
                    Description = b.Description,
                    Title = b.Title,
                    ImageId = b.ImageId,
                    ImageUrl = b.Image != null ? b.Image.Url : null,
                })
                .ToListAsync();

            return new PagedResponse<BannerViewModel>
            {
                Data = banners,
                TotalRecords = totalRecords,
                CurrentPage = page,
                PerPage = perPage
            };
        }

        public async Task<BannerViewModel> GetByIdAsync(int id)
        {
            return await _context.Banners
                .Where(b => b.Id == id &&  b.IsDeleted != true)
                .Select(b => new BannerViewModel
                {
                    Id = b.Id,
                    Description = b.Description,
                    Title = b.Title,
                    ImageId = b.ImageId,
                    ImageUrl = b.Image != null ? b.Image.Url : null,
                })
                .FirstOrDefaultAsync();
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
