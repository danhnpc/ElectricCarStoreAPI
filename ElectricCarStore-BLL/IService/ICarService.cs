using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface ICarService
    {
        Task<PagedResponse<Car>> GetAllCarsAsync(int page, int perPage);
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> CreateCarAsync(CarPostModel carModel);
        Task<Car> CreateCarDetailAsync(CarDetailPostModel model);
        Task<CarDetailViewModel> GetCarDetailByIdAsync(int id);
        Task<Car> UpdateCarAsync(int id, CarPostModel carModel);
        Task<bool> DeleteCarAsync(int id);
        Task<(IEnumerable<Car> Cars, int TotalCount, int TotalPages)> GetPagedCarsAsync(int pageNumber, int pageSize, string searchTerm = null);
    }

}
