using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetAllNews(int page = 1, int perPage = 10, bool? isAboutUs = null)
        {
            var news = await _newsService.GetAllNewsAsync(page, perPage, isAboutUs);
            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            try
            {
                var news = await _newsService.GetNewsByIdAsync(id);
                return Ok(news);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<News>> CreateNews(NewsPostModel newsModel)
        {
            try
            {
                var createdNews = await _newsService.CreateNewsAsync(newsModel);
                return CreatedAtAction(nameof(GetNews), new { id = createdNews.Id }, createdNews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, NewsPostModel newsModel)
        {
            try
            {
                var updatedNews = await _newsService.UpdateNewsAsync(id, newsModel);
                return Ok(updatedNews);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
                var result = await _newsService.DeleteNewsAsync(id);
                return Ok(new { success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
