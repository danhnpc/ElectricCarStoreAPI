using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(NewsPostModel newsModel);
        Task<News> UpdateNewsAsync(int id, NewsPostModel newsModel);
        Task<bool> DeleteNewsAsync(int id);
    }

}
