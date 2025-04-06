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
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                throw new KeyNotFoundException($"Xe với ID {id} không tồn tại.");

            return car;
        }

        public async Task<Car> CreateCarAsync(CarPostModel carModel)
        {
            var car = new Car
            {
                Name = carModel.Name,
                DescriptionA = carModel.DescriptionA,
                DescriptionB = carModel.DescriptionB,
                IsDeleted = false
            };

            return await _carRepository.AddAsync(car);
        }

        public async Task<Car> UpdateCarAsync(int id, CarPostModel carModel)
        {
            var existingCar = await _carRepository.GetByIdAsync(id);
            if (existingCar == null)
                throw new KeyNotFoundException($"Xe với ID {id} không tồn tại.");

            // Cập nhật các thuộc tính từ model
            existingCar.Name = carModel.Name;
            existingCar.DescriptionA = carModel.DescriptionA;
            existingCar.DescriptionB = carModel.DescriptionB;

            return await _carRepository.UpdateAsync(existingCar);
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var exists = await _carRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Xe với ID {id} không tồn tại.");

            return await _carRepository.DeleteAsync(id);
        }

        public async Task<(IEnumerable<Car> Cars, int TotalCount, int TotalPages)> GetPagedCarsAsync(int pageNumber, int pageSize, string searchTerm = null)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1 || pageSize > 100)
                pageSize = 10;

            var (cars, totalCount) = await _carRepository.GetPagedAsync(pageNumber, pageSize, searchTerm);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return (cars, totalCount, totalPages);
        }
    }

}
