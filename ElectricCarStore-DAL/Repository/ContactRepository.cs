using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ElectricCarStoreContext _context;

        public ContactRepository(ElectricCarStoreContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<Contact>> GetAllAsync(int page, int perPage)
        {
            var query = _context.Contacts.Where(x => x.IsDeleted != true);
            var totalCount = await query.CountAsync();

            var contacts = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();

            return new PagedResponse<Contact>
            {
                Data = contacts,
                TotalRecords = totalCount,
                CurrentPage = page,
                PerPage = perPage
            };
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            contact.IsDeleted = false;

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            // Soft delete
            contact.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Contacts
                .AnyAsync(c => c.Id == id && c.IsDeleted != true);
        }

        public async Task<IEnumerable<Contact>> GetByCarIdAsync(int carId)
        {
            return await _context.Contacts
                .Where(c => c.CarId == carId && c.IsDeleted != true)
                .ToListAsync();
        }
    }

}
