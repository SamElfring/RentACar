using RentACar.Models.ViewModels;
using System.Collections.Generic;

namespace RentACar.Services
{
    public interface IInvoiceService
    {
        public List<InvoiceViewModel> GetUserInvoices();
        public List<InvoiceViewModel> GetInvoices();
    }
}
