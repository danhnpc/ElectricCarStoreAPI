

using ElectricCarStore_DAL.Models.Model;

namespace ElectricCarStore_DAL.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
