namespace RentACar.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public ApplicationUser User { get; set; }

        public Car Car { get; set; }

        public InvoiceRule InvoiceRule { get; set; }

        public Invoice Invoice { get; set; }

        public decimal RentPrice { get; set; }
    }
}
