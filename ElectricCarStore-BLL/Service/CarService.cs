using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.IRepository;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Repository;
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
        private readonly ICarTypeRepository _carTypeRepository;

        public CarService(
            ICarRepository carRepository,
            ICarTypeRepository carTypeRepository
            )
        {
            _carRepository = carRepository;
            _carTypeRepository = carTypeRepository;
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

        public async Task<Car> CreateCarDetailAsync(CarDetailPostModel model)
        {
            // Tạo đối tượng Car mới
            var car = new Car
            {
                Name = model.Name,
                DescriptionA = model.DescriptionA,
                DescriptionB = model.DescriptionB,
                IsDeleted = false
            };

            // Lưu Car và liên kết với các hình ảnh
            var createdCar = await _carRepository.CreateCarWithImagesAsync(car, model.ImageIds);

            // Tạo các loại xe (CarType) nếu có
            if (model.CarTypes != null && model.CarTypes.Count > 0)
            {
                foreach (var carTypeModel in model.CarTypes)
                {
                    var carType = new CarType
                    {
                        CarId = createdCar.Id,
                        TypeName = carTypeModel.TypeName,
                        Price = carTypeModel.Price,
                        IsDeleted = false
                    };

                    await _carTypeRepository.AddAsync(carType);
                }
            }

            return createdCar;
        }

        public async Task<CarDetailViewModel> GetCarDetailByIdAsync(int id)
        {
            var car = await _carRepository.GetCarDetailByIdAsync(id);
            if (car == null)
                throw new KeyNotFoundException($"Xe với ID {id} không tồn tại.");

            // Lấy các loại xe
            var carTypes = await _carTypeRepository.GetByCarIdAsync(id);

            // Chuyển đổi sang ViewModel
            var carDetailViewModel = new CarDetailViewModel
            {
                Id = car.Id,
                Name = car.Name,
                DescriptionA = car.DescriptionA,
                DescriptionB = car.DescriptionB,
                Images = car.CarImages.Select(ci => new ImageViewModel
                {
                    Id = ci.Image.Id,
                    Url = ci.Image.Url
                }).ToList(),
                CarTypes = carTypes.Select(ct => new CarTypeViewModel
                {
                    Id = ct.Id,
                    TypeName = ct.TypeName,
                    Price = ct.Price ?? 0
                }).ToList()
            };

            return carDetailViewModel;
        }
    }

}
