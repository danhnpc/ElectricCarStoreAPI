using ElectricCarStore_BLL.IService;
using ElectricCarStore_BLL.Security;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.PostModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly string _secretKey;

        public UserService(
            IConfiguration configuration,
            IUserRepository userRepository
            )
        {
            _configuration = configuration;
            _userRepository = userRepository;

            _secretKey = _configuration.GetValue<string>("Jwt:SignKey");
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> AddUserAsync(LoginRequest user)
        {
            return await _userRepository.AddAsync(new User
            {
                Username = user.Username,
                Password = CryptoService.AESHash(user.Password, _secretKey),
                IsDeleted = false
            });
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task DisableUserAsync(int id)
        {
            await _userRepository.DisableAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}
