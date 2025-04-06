using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.Service
{
    public class CarTypeService : ICarTypeService
    {
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly ICarRepository _carRepository;

        public CarTypeService(ICarTypeRepository carTypeRepository, ICarRepository carRepository)
        {
            _carTypeRepository = carTypeRepository;
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<CarType>> GetAllCarTypesAsync()
        {
            return await _carTypeRepository.GetAllAsync();
        }

        public async Task<CarType> GetCarTypeByIdAsync(int id)
        {
            var carType = await _carTypeRepository.GetByIdAsync(id);
            if (carType == null)
                throw new KeyNotFoundException($"Loại xe với ID {id} không tồn tại.");

            return carType;
        }

        public async Task<IEnumerable<CarType>> GetCarTypesByCarIdAsync(int carId)
        {
            // Kiểm tra xe có tồn tại không
            var carExists = await _carRepository.ExistsAsync(carId);
            if (!carExists)
                throw new KeyNotFoundException($"Xe với ID {carId} không tồn tại.");

            return await _carTypeRepository.GetByCarIdAsync(carId);
        }

        public async Task<CarType> CreateCarTypeAsync(CarTypePostModel carTypeModel)
        {
            // Kiểm tra xe có tồn tại không
            var carExists = await _carRepository.ExistsAsync(carTypeModel.CarId);
            if (!carExists)
                throw new KeyNotFoundException($"Xe với ID {carTypeModel.CarId} không tồn tại.");

            var carType = new CarType
            {
                CarId = carTypeModel.CarId,
                TypeName = carTypeModel.TypeName,
                Price = carTypeModel.Price,
                IsDeleted = false
            };

            return await _carTypeRepository.AddAsync(carType);
        }

        public async Task<CarType> UpdateCarTypeAsync(int id, CarTypePostModel carTypeModel)
        {
            var existingCarType = await _carTypeRepository.GetByIdAsync(id);
            if (existingCarType == null)
                throw new KeyNotFoundException($"Loại xe với ID {id} không tồn tại.");

            // Kiểm tra xe có tồn tại không
            var carExists = await _carRepository.ExistsAsync(carTypeModel.CarId);
            if (!carExists)
                throw new KeyNotFoundException($"Xe với ID {carTypeModel.CarId} không tồn tại.");

            // Cập nhật các thuộc tính từ model
            existingCarType.CarId = carTypeModel.CarId;
            existingCarType.TypeName = carTypeModel.TypeName;
            existingCarType.Price = carTypeModel.Price;

            return await _carTypeRepository.UpdateAsync(existingCarType);
        }

        public async Task<bool> DeleteCarTypeAsync(int id)
        {
            var exists = await _carTypeRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Loại xe với ID {id} không tồn tại.");

            return await _carTypeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CarTypeViewModel>> GetCarTypesWithCarInfoAsync()
        {
            var carTypes = await _carTypeRepository.GetAllAsync();
            var result = new List<CarTypeViewModel>();

            foreach (var carType in carTypes)
            {
                var car = await _carRepository.GetByIdAsync(carType.CarId ?? 0);
                result.Add(new CarTypeViewModel
                {
                    Id = carType.Id,
                    CarId = carType.CarId ?? 0,
                    CarName = car?.Name ?? "Unknown",
                    TypeName = carType.TypeName,
                    Price = carType.Price ?? 0
                });
            }

            return result;
        }
    }

}
