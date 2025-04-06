using ElectricCarStore_BLL.IService;
using ElectricCarStore_DAL.Models.Model;
using ElectricCarStore_DAL.Models.PostModel;
using ElectricCarStore_DAL.Models.QueryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricCarStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypesController : ControllerBase
    {
        private readonly ICarTypeService _carTypeService;

        public CarTypesController(ICarTypeService carTypeService)
        {
            _carTypeService = carTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarType>>> GetAllCarTypes()
        {
            var carTypes = await _carTypeService.GetAllCarTypesAsync();
            return Ok(carTypes);
        }

        [HttpGet("with-car-info")]
        public async Task<ActionResult<IEnumerable<CarTypeViewModel>>> GetCarTypesWithCarInfo()
        {
            var carTypesInfo = await _carTypeService.GetCarTypesWithCarInfoAsync();
            return Ok(carTypesInfo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarType>> GetCarType(int id)
        {
            try
            {
                var carType = await _carTypeService.GetCarTypeByIdAsync(id);
                return Ok(carType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("car/{carId}")]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarTypesByCar(int carId)
        {
            try
            {
                var carTypes = await _carTypeService.GetCarTypesByCarIdAsync(carId);
                return Ok(carTypes);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarType>> CreateCarType(CarTypePostModel carTypeModel)
        {
            try
            {
                var createdCarType = await _carTypeService.CreateCarTypeAsync(carTypeModel);
                return CreatedAtAction(nameof(GetCarType), new { id = createdCarType.Id }, createdCarType);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarType(int id, CarTypePostModel carTypeModel)
        {
            try
            {
                var updatedCarType = await _carTypeService.UpdateCarTypeAsync(id, carTypeModel);
                return Ok(updatedCarType);
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
        public async Task<IActionResult> DeleteCarType(int id)
        {
            try
            {
                var result = await _carTypeService.DeleteCarTypeAsync(id);
                return Ok(new { success = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
