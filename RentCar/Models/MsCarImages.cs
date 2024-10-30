using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class MsCarImages
    {
        [Key]
        [MaxLength(36)]
        public string Image_car_id { get; set; }

        [ForeignKey("MsCar")]
        [MaxLength(36)]
        public string Car_id { get; set; }

        [MaxLength(2000)]
        public string image_link { get; set; }

        public MsCar Car { get; set; }
    }
}
