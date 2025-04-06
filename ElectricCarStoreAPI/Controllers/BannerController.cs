using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanners()
        {
            var banners = await _bannerService.GetAllBannersAsync();
            return Ok(banners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> GetBanner(int id)
        {
            try
            {
                var banner = await _bannerService.GetBannerByIdAsync(id);
                return Ok(new DefaultResponseContext { Message = "Success", Data = banner});
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Banner>> CreateBanner(BannerPostModel bannerModel)
        {
            try
            {
                var createdBanner = await _bannerService.CreateBannerAsync(bannerModel);
                return CreatedAtAction(nameof(GetBanner), new { id = createdBanner.Id }, createdBanner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(int id, BannerPostModel bannerModel)
        {
            try
            {
                var updatedBanner = await _bannerService.UpdateBannerAsync(id, bannerModel);
                return Ok(updatedBanner);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            try
            {
                var result = await _bannerService.DeleteBannerAsync(id);
                return Ok(new { success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
