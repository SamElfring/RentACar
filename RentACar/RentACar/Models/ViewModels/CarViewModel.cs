using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models.ViewModels
{
    public class CarViewModel
    {
        [DisplayName("Kenteken")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string LicensePlate { get; set; }

        [DisplayName("Merk")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Brand { get; set; }

        [DisplayName("Type")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Type { get; set; }

        [DisplayName("Dag prijs")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string DayPrice { get; set; }
    }
}
