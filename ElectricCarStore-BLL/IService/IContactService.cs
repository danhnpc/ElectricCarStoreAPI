using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<Contact> CreateContactAsync(ContactPostModel contactModel);
        Task<Contact> UpdateContactAsync(int id, ContactPostModel contactModel);
        Task<bool> DeleteContactAsync(int id);
        Task<IEnumerable<Contact>> GetContactsByCarIdAsync(int carId);
    }

}
