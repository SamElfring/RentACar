using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Car
    {
        [Key]
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string DayPrice { get; set; }
    }
}
