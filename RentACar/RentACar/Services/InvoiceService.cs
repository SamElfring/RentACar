using RentACar.Models;
using RentACar.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;

        public InvoiceService(ApplicationDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public List<InvoiceViewModel> GetUserInvoices()
        {
            string userId = _userService.GetUserId();

            return (from invoiceRule in _db.InvoiceRules
                    join invoice in _db.Invoices on invoiceRule.Invoice.Number equals invoice.Number
                    join user in _db.Users on invoice.Customer.Id equals userId
                    join car in _db.Cars on invoiceRule.Car.LicensePlate equals car.LicensePlate
                    where user.Id == userId
                    select new InvoiceViewModel
                    {
                        InvoiceRule = invoiceRule,
                        Invoice = invoice,
                        Car = car,
                        User = user,
                        RentPrice = (car.DayPrice * ((invoiceRule.EndDate - invoiceRule.StartDate).Days))
                    }).ToList();
        }

        public List<InvoiceViewModel> GetInvoices()
        {
            return (from invoiceRule in _db.InvoiceRules
                    join invoice in _db.Invoices on invoiceRule.Invoice.Number equals invoice.Number
                    join user in _db.Users on invoice.Customer.Id equals user.Id
                    join car in _db.Cars on invoiceRule.Car.LicensePlate equals car.LicensePlate
                    select new InvoiceViewModel
                    {
                        InvoiceRule = invoiceRule,
                        Invoice = invoice,
                        Car = car,
                        User = user,
                        RentPrice = (car.DayPrice * ((invoiceRule.EndDate - invoiceRule.StartDate).Days))
                    }).ToList();
        }
    }
}
