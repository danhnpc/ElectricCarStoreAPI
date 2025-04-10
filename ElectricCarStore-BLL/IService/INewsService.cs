using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface INewsService
    {
        Task<PagedResponse<NewsViewModel>> GetAllNewsAsync(int page, int perPage, bool? isAboutUs = null);
        Task<News> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(NewsPostModel newsModel);
        Task<News> UpdateNewsAsync(int id, NewsPostModel newsModel);
        Task<bool> DeleteNewsAsync(int id);
    }

}
