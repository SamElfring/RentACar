using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models.ViewModels
{
    public class InvoiceRuleViewModel
    {
        [DisplayName("Auto")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        public string Car { get; set; }

        [DisplayName("Begin datum")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DisplayName("Eind datum")]
        [Required(ErrorMessage = "{0} is een verplicht veld!")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
