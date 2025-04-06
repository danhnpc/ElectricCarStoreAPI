using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;


namespace ElectricCarStore_BLL.IService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(LoginRequest user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task DisableUserAsync(int id);

    }
}
