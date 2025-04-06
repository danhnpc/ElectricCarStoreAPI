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
    public class NewsRepository : INewsRepository
    {
        private readonly ElectricCarStoreContext _context;

        public NewsRepository(ElectricCarStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News
                .Where(n => n.IsDeleted != true)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News
                .FirstOrDefaultAsync(n => n.Id == id && n.IsDeleted != true);
        }

        public async Task<News> AddAsync(News news)
        {
            news.CreatedDate = DateTime.UtcNow;
            news.IsDeleted = false;

            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News> UpdateAsync(News news)
        {
            _context.Entry(news).State = EntityState.Modified;

            // Đảm bảo không thay đổi CreatedDate
            _context.Entry(news).Property(x => x.CreatedDate).IsModified = false;

            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null)
                return false;

            // Soft delete
            news.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.News
                .AnyAsync(n => n.Id == id && n.IsDeleted != true);
        }
    }

}
