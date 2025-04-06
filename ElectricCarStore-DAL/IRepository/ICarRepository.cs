using ElectricCarStore_DAL.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.IRepository
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task<Car> AddAsync(Car car);
        Task<Car> UpdateAsync(Car car);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<(IEnumerable<Car> Cars, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string searchTerm = null);
    }

}
