using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_DAL.IRepository
{
    public interface ICarRepository
    {
        Task<PagedResponse<Car>> GetAllAsync(int page, int perPage);
        Task<Car> GetByIdAsync(int id);
        Task<Car> AddAsync(Car car);
        Task<Car> CreateCarWithImagesAsync(Car car, List<int> imageIds);
        Task<Car> GetCarDetailByIdAsync(int id);
        Task<Car> UpdateAsync(Car car);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<(IEnumerable<Car> Cars, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string searchTerm = null);
    }

}
