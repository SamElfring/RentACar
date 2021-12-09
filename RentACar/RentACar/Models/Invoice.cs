using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public DateTime Date { get; set; }

        public ApplicationUser Customer { get; set; }

        public ApplicationUser Employee { get; set; }
    }
}
