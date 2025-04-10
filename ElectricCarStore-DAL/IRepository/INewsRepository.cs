using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.IRepository
{
    public interface INewsRepository
    {
        Task<PagedResponse<NewsViewModel>> GetAllAsync(int page = 1, int perPage = 10, bool? isAboutUs = null);
        Task<News> GetByIdAsync(int id);
        Task<News> AddAsync(News news);
        Task<News> UpdateAsync(News news);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
