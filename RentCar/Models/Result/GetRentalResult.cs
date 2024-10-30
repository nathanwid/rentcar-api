using System;

namespace RentCar.Models.Result;

public class GetRentalResult
{
    public string Rental_id { get; set; }
    public DateTime rental_date { get; set; }
    public DateTime return_date { get; set; }
    public decimal total_price { get; set; }
    public bool payment_status { get; set; }
    public string customer_name { get; set; }
    public string car_name { get; set; }
}
