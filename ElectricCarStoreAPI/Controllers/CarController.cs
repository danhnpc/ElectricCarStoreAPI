﻿using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using ElectricCarStore_DAL.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars(int page = 1, int perPage = 10)
        {
            var cars = await _carService.GetAllCarsAsync(page, perPage);
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                return Ok(car);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult> GetPagedCars([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchTerm = null)
        {
            var (cars, totalCount, totalPages) = await _carService.GetPagedCarsAsync(pageNumber, pageSize, searchTerm);

            return Ok(new
            {
                data = cars,
                pageNumber,
                pageSize,
                totalCount,
                totalPages,
                searchTerm
            });
        }

        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(CarPostModel carModel)
        {
            try
            {
                var createdCar = await _carService.CreateCarAsync(carModel);
                return CreatedAtAction(nameof(GetCar), new { id = createdCar.Id }, createdCar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarPostModel carModel)
        {
            try
            {
                var updatedCar = await _carService.UpdateCarAsync(id, carModel);
                return Ok(updatedCar);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var result = await _carService.DeleteCarAsync(id);
                return Ok(new { success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("detail")]
        public async Task<ActionResult<Car>> CreateCarDetail(CarDetailPostModel model)
        {
            try
            {
                var createdCar = await _carService.CreateCarDetailAsync(model);
                return Ok( new DefaultResponseContext { Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<CarDetailViewModel>> GetCarDetail(int id)
        {
            try
            {
                var carDetail = await _carService.GetCarDetailByIdAsync(id);
                return Ok(carDetail);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
