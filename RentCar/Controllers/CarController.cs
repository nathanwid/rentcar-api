using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models.Result;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCarResult>>> Get()
        {
            var cars = await _context.MsCar
                .Where(x => x.status == true) // Available cars for rent
                .Select(x => new GetCarResult
                {
                    Car_id = x.Car_id,
                    name = x.name,
                    model = x.model,
                    year = x.year,
                    license_plate = x.license_plate,
                    number_of_car_seats = x.number_of_car_seats,
                    transmission = x.transmission,
                    price_per_day = x.price_per_day,
                    status = x.status
                }).ToListAsync();

            var response = new ApiResponse<IEnumerable<GetCarResult>> 
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = cars
            };

            return Ok(response);
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<IEnumerable<GetCarResult>>> Get(int year)
        {
            var cars = await _context.MsCar
                .Where(x => x.status == true) // Available cars for rent
                .Where(x => x.year == year)
                .Select(x => new GetCarResult
                {
                    Car_id = x.Car_id,
                    name = x.name,
                    model = x.model,
                    year = x.year,
                    license_plate = x.license_plate,
                    number_of_car_seats = x.number_of_car_seats,
                    transmission = x.transmission,
                    price_per_day = x.price_per_day,
                    status = x.status
                }).ToListAsync();

            var response = new ApiResponse<IEnumerable<GetCarResult>> 
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = cars
            };

            return Ok(response);
        }
    }
}
