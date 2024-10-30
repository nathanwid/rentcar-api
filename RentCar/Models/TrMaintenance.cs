using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class TrMaintenance
    {
        [Key]
        [MaxLength(36)]
        public string Maintenance_id { get; set; }

        public DateTime maintenance_date { get; set; }

        [MaxLength(4000)]
        public string description { get; set; }

        public decimal cost { get; set; }

        [ForeignKey("MsCar")]
        [MaxLength(36)]
        public string Car_id { get; set; }

        [ForeignKey("MsEmployee")]
        [MaxLength(36)]
        public string Employee_id { get; set; }

        public MsCar Car { get; set; }

        public MsEmployee Employee { get; set; }
    }
}
