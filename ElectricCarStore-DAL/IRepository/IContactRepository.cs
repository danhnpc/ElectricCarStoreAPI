using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.IRepository
{
    public interface IContactRepository
    {
        Task<PagedResponse<Contact>> GetAllAsync(int page, int perPage);
        Task<Contact> GetByIdAsync(int id);
        Task<Contact> AddAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Contact>> GetByCarIdAsync(int carId);
    }

}
