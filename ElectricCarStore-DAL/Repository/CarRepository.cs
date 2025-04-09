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
    public class CarRepository : ICarRepository
    {
        private readonly ElectricCarStoreContext _context;

        public CarRepository(ElectricCarStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars
                .Where(c => c.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);
        }

        public async Task<Car> AddAsync(Car car)
        {
            car.IsDeleted = false;

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return false;

            // Soft delete
            car.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Cars
                .AnyAsync(c => c.Id == id && c.IsDeleted != true);
        }

        public async Task<(IEnumerable<Car> Cars, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string searchTerm = null)
        {
            var query = _context.Cars.Where(c => c.IsDeleted != true);

            // Áp dụng tìm kiếm nếu có
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm) ||
                                         c.DescriptionA.Contains(searchTerm) ||
                                         c.DescriptionB.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();

            var cars = await query
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (cars, totalCount);
        }

        public Task<Car> AddCarDetailAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public async Task<Car> CreateCarWithImagesAsync(Car car, List<int> imageIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Thêm Car
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();

                // Thêm CarImage cho mỗi imageId
                if (imageIds != null && imageIds.Count > 0)
                {
                    foreach (var imageId in imageIds)
                    {
                        // Kiểm tra xem Image có tồn tại không
                        var imageExists = await _context.Images.AnyAsync(i => i.Id == imageId && i.IsDeleted != true);
                        if (imageExists)
                        {
                            var carImage = new CarImage
                            {
                                CarId = car.Id,
                                ImageId = imageId,
                                IsDeleted = false
                            };

                            _context.CarImages.Add(carImage);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return car;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Car> GetCarDetailByIdAsync(int id)
        {
            return await _context.Cars
                .Include(c => c.CarImages)
                    .ThenInclude(ci => ci.Image)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);
        }
    }

}
