using ElectricCarStore_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ElectricCarStoreContext _dbContext;

        public TestController(
            IConfiguration configuration,
            ElectricCarStoreContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                var dbConfig = _configuration.GetSection("DBConnect:ConnectString");

                string server = dbConfig["Server"];
                string uid = dbConfig["Uid"];
                string pwd = dbConfig["Pwd"];
                string database = dbConfig["Database"];

                await _dbContext.Database.CanConnectAsync();
                return Ok(new
                {
                    Message = "✅ Kết nối database thành công!",
                    Server = server,
                    Uid = uid,
                    Pwd = pwd,
                    Database = database
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Không thể kết nối database: {ex.Message}");
            }
        }
    }
}
