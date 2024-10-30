using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class TrRental
    {
        [Key]
        [MaxLength(36)]
        public string Rental_id { get; set; }

        public DateTime rental_date { get; set; }

        public DateTime return_date { get; set; }

        public decimal total_price { get; set; }

        public bool payment_status { get; set; }

        [ForeignKey("MsCustomer")]
        [MaxLength(36)]
        public string customer_id { get; set; }

        [ForeignKey("MsCar")]
        [MaxLength(36)]
        public string car_id { get; set; }

        public MsCustomer Customer { get; set; }

        public MsCar Car { get; set; }
    }
}
