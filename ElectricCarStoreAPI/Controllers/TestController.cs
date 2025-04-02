using ElectricCarStore_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ElectricCarStoreContext _dbContext;

        public TestController(ElectricCarStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                await _dbContext.Database.CanConnectAsync();
                return Ok("✅ Kết nối database thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Không thể kết nối database: {ex.Message}");
            }
        }
    }
}
