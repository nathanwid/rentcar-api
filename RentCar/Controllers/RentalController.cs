using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using RentCar.Models.Request;
using RentCar.Models.Result;

namespace RentCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RentalController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRentalResult>>> Get()
        {
            var rentals = await _context.TrRental
                .Include(x => x.Car)
                .Include(x => x.Customer)
                .Select(x => new GetRentalResult
                {
                    Rental_id = x.Rental_id,
                    rental_date = x.rental_date,
                    return_date = x.return_date,
                    total_price = x.total_price,
                    payment_status = x.payment_status,
                    customer_name = x.Customer.name,
                    car_name = x.Car.name
                }).ToListAsync();

            var response = new ApiResponse<IEnumerable<GetRentalResult>> 
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = rentals
            };

            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentalRequest request)
        {
            try
            {
                var isCarExists = await _context.MsCar.Where(x => x.Car_id == request.car_id).AnyAsync();
                if(!isCarExists)
                {
                    throw new KeyNotFoundException("Car data not found");
                }

                var isCustomerExists = await _context.MsCustomer.Where(x => x.Customer_id == request.customer_id).AnyAsync();
                if(!isCustomerExists)
                {
                    throw new KeyNotFoundException("Customer data not found");
                }

                var topRentalId = await _context.TrRental
                    .OrderByDescending(x => x.Rental_id)
                    .Select(x => x.Rental_id)
                    .FirstOrDefaultAsync();

                var substringId = topRentalId?.Substring(3);

                int currentId = string.IsNullOrEmpty(substringId) ? 0 : int.Parse(substringId);

                currentId += 1;
                var newRentalId = $"RTD{currentId:D3}";

                var rentalData = new TrRental
                {
                    Rental_id = newRentalId,
                    rental_date = request.rental_date,
                    return_date = request.return_date,
                    total_price = request.total_price,
                    payment_status = request.payment_status,
                    customer_id = request.customer_id,
                    car_id = request.car_id
                };
                
                _context.TrRental.Add(rentalData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status201Created,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Rental data saved successfully"
                };

                return Ok(response);
            }
            catch(Exception e)
            {
                var response = new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = e.Message
                };
                return BadRequest(response);
            }
            
        }
    }
}