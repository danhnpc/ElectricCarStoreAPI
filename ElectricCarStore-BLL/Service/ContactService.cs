using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
                throw new KeyNotFoundException($"Liên hệ với ID {id} không tồn tại.");

            return contact;
        }

        public async Task<Contact> CreateContactAsync(ContactPostModel contactModel)
        {
            var contact = new Contact
            {
                FullName = contactModel.FullName,
                PhoneNumber = contactModel.PhoneNumber,
                Email = contactModel.Email,
                Address = contactModel.Address,
                Title = contactModel.Title,
                Content = contactModel.Content,
                CarId = contactModel.CarId,
                IsContact = contactModel.IsContact,
                IsDeleted = false
            };

            return await _contactRepository.AddAsync(contact);
        }

        public async Task<Contact> UpdateContactAsync(int id, ContactPostModel contactModel)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id);
            if (existingContact == null)
                throw new KeyNotFoundException($"Liên hệ với ID {id} không tồn tại.");

            // Cập nhật các thuộc tính từ model
            existingContact.FullName = contactModel.FullName;
            existingContact.PhoneNumber = contactModel.PhoneNumber;
            existingContact.Email = contactModel.Email;
            existingContact.Address = contactModel.Address;
            existingContact.Title = contactModel.Title;
            existingContact.Content = contactModel.Content;
            existingContact.CarId = contactModel.CarId;
            existingContact.IsContact = contactModel.IsContact;

            return await _contactRepository.UpdateAsync(existingContact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var exists = await _contactRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Liên hệ với ID {id} không tồn tại.");

            return await _contactRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetContactsByCarIdAsync(int carId)
        {
            return await _contactRepository.GetByCarIdAsync(carId);
        }
    }

}
