using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;

namespace ElectricCarStore_BLL.Service
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(bool? isAboutUs = null)
        {
            return await _newsRepository.GetAllAsync(isAboutUs);
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
                throw new KeyNotFoundException($"Tin tức với ID {id} không tồn tại.");

            return news;
        }

        public async Task<News> CreateNewsAsync(NewsPostModel newsModel)
        {
            var news = new News
            {
                Name = newsModel.Name,
                Desc = newsModel.Desc,
                ImageId = newsModel.ImageId,
                Content = newsModel.Content,
                // CreatedDate sẽ được thiết lập trong repository
                IsDeleted = false,
                IsAboutUs = newsModel.IsAboutUs,
            };

            return await _newsRepository.AddAsync(news);
        }

        public async Task<News> UpdateNewsAsync(int id, NewsPostModel newsModel)
        {
            var existingNews = await _newsRepository.GetByIdAsync(id);
            if (existingNews == null)
                throw new KeyNotFoundException($"Tin tức với ID {id} không tồn tại.");

            // Cập nhật các thuộc tính từ model
            existingNews.Name = newsModel.Name;
            existingNews.Desc = newsModel.Desc;
            existingNews.ImageId = newsModel.ImageId;
            existingNews.Content = newsModel.Content;
            // Giữ nguyên CreatedDate và IsDeleted

            return await _newsRepository.UpdateAsync(existingNews);
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            var exists = await _newsRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Tin tức với ID {id} không tồn tại.");

            return await _newsRepository.DeleteAsync(id);
        }
    }

}
