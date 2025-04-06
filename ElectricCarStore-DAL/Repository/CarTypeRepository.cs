using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models;
using ElectricCarStore_DAL.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.Repository
{
    public class CarTypeRepository : ICarTypeRepository
    {
        private readonly ElectricCarStoreContext _context;

        public CarTypeRepository(ElectricCarStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarType>> GetAllAsync()
        {
            return await _context.CarTypes
                .Where(ct => ct.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<CarType> GetByIdAsync(int id)
        {
            return await _context.CarTypes
                .FirstOrDefaultAsync(ct => ct.Id == id && ct.IsDeleted != true);
        }

        public async Task<IEnumerable<CarType>> GetByCarIdAsync(int carId)
        {
            return await _context.CarTypes
                .Where(ct => ct.CarId == carId && ct.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<CarType> AddAsync(CarType carType)
        {
            carType.IsDeleted = false;

            _context.CarTypes.Add(carType);
            await _context.SaveChangesAsync();
            return carType;
        }

        public async Task<CarType> UpdateAsync(CarType carType)
        {
            _context.Entry(carType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return carType;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
                return false;

            // Soft delete
            carType.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CarTypes
                .AnyAsync(ct => ct.Id == id && ct.IsDeleted != true);
        }
    }

}
