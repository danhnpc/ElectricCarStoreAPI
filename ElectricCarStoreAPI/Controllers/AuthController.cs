using ElectricCarStore_BLL.IService;
using ElectricCarStore_BLL.Security;
using ElectricCarStore_BLL.Service;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;
        private readonly IUserService _userService;

        private readonly string _secretKey;

        public AuthController(
            IConfiguration configuration,
            AuthService authService,
            IUserService userService
            )
        {
            _configuration = configuration;
            _authService = authService;
            _userService = userService;

            _secretKey = _configuration.GetValue<string>("Jwt:SignKey");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(model?.Username);
                if (user == null)
                    return NotFound(new DefaultResponseContext { Message = "Username does not existed" });
                if (CryptoService.AESHash(model.Password, _secretKey) != user.Password)
                    return NotFound(new DefaultResponseContext { Message = "Password incorrect" });

                AccessToken JwtToken = _authService.Login(user);

                var data = new
                {
                    Token = JwtToken,
                    User = user
                };

                return Ok(new DefaultResponseContext { Message = "Success", Data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error. Message = {ex.Message}");
                throw;
            }
        }
    }
}
