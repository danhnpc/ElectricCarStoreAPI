using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.IService
{
    public interface ICarTypeService
    {
        Task<IEnumerable<CarType>> GetAllCarTypesAsync();
        Task<CarType> GetCarTypeByIdAsync(int id);
        Task<IEnumerable<CarType>> GetCarTypesByCarIdAsync(int carId);
        Task<CarType> CreateCarTypeAsync(CarTypePostModel carTypeModel);
        Task<CarType> UpdateCarTypeAsync(int id, CarTypePostModel carTypeModel);
        Task<bool> DeleteCarTypeAsync(int id);
        Task<IEnumerable<CarTypeViewModel>> GetCarTypesWithCarInfoAsync();
    }

}
