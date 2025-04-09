using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface INewsService
    {
        Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(bool? isAboutUs = null);
        Task<News> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(NewsPostModel newsModel);
        Task<News> UpdateNewsAsync(int id, NewsPostModel newsModel);
        Task<bool> DeleteNewsAsync(int id);
    }

}
