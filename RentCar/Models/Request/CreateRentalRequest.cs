using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.Request;

public class CreateRentalRequest
{
    [Required]
    public DateTime rental_date { get; set; }
    [Required]
    public DateTime return_date { get; set; }
    [Required]
    public decimal total_price { get; set; }
    [Required]
    public bool payment_status { get; set; }
    [Required]
    public string customer_id { get; set; }
    [Required]
    public string car_id { get; set; }
}
