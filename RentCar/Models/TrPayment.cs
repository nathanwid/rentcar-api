using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class TrPayment
    {
        [Key]
        [MaxLength(36)]
        public string Payment_id { get; set; }

        public DateTime payment_date { get; set; }

        public decimal amount { get; set; }

        [MaxLength(100)]
        public string payment_method { get; set; }

        [ForeignKey("TrRental")]
        [MaxLength(36)]
        public string Rental_id { get; set; }

        public TrRental Rental { get; set; }
    }
}
