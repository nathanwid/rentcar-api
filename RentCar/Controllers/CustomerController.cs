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
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // POST
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        RequestMethod = HttpContext.Request.Method,
                        Data = "Email and password are required."
                    });
                }

                var customerData = await _context.MsCustomer
                    .Where(x => x.email == request.email && x.password == request.password) 
                    .Select(x => new GetCustomerResult
                    {
                        Customer_id = x.Customer_id,
                        email = x.email,
                        password = x.password,
                        name = x.name,
                        phone_number = x.phone_number,
                        address = x.address,
                        driver_license_number = x.driver_license_number
                    })
                    .FirstOrDefaultAsync();

                if (customerData == null)
                {
                    return Unauthorized(new ApiResponse<string>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        RequestMethod = HttpContext.Request.Method,
                        Data = "Invalid email or password."
                    });
                }

                var response = new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Customer data found"
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    RequestMethod = HttpContext.Request.Method,
                    Data = e.Message
                };
                return BadRequest(response);
            }
        }



        // POST
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerRequest request)
        {
            try
            {
                var topCustomerId = await _context.MsCustomer
                    .OrderByDescending(x => x.Customer_id)
                    .Select(x => x.Customer_id)
                    .FirstOrDefaultAsync();

                var substringId = topCustomerId?.Substring(3);

                var currentId = int.Parse(substringId);

                currentId += 1;
                var newCustomerId = currentId.ToString("D3");

                var customerData = new MsCustomer
                {
                    Customer_id = $"CUS{newCustomerId}",
                    email = request.email,
                    password = request.password,
                    name = request.name,
                    phone_number = request.phone_number,
                    address = request.address,
                    driver_license_number = request.driver_license_number
                };
                
                _context.MsCustomer.Add(customerData);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status201Created,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Customer data saved successfully"
                };

                return Ok(response);
            }
            catch(Exception e)
            {
                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = e.Message
                };
                return BadRequest(response);
            }
            
        }
    }
}
