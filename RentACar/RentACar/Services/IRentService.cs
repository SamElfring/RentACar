using RentACar.Models;
using RentACar.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface IRentService
    {
        public Task<int> AddRent(InvoiceRuleViewModel model);
        public List<InvoiceRule> GetUserHiredCars();
    }
}
