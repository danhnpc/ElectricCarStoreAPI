using ElectricCarStore_DAL.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.IRepository
{
    public interface ICarTypeRepository
    {
        Task<IEnumerable<CarType>> GetAllAsync();
        Task<CarType> GetByIdAsync(int id);
        Task<IEnumerable<CarType>> GetByCarIdAsync(int carId);
        Task<CarType> AddAsync(CarType carType);
        Task<CarType> UpdateAsync(CarType carType);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }

}
