using RentACar.Models;
using RentACar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public class RentService : IRentService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICarService _carService;
        private readonly IUserService _userService;

        public RentService(ApplicationDbContext db, ICarService carService, IUserService userService)
        {
            _db = db;
            _userService = userService;
            _carService = carService;
        }

        public async Task<int> AddRent(InvoiceRuleViewModel model)
        {
            // Get car
            Car car = _carService.GetCarFromLicensePlate(model.Car);

            // Create invoice
            Invoice invoice = new Invoice()
            {
                Date = DateTime.Now,
                Customer = await _userService.GetUser(),
                Employee = _userService.GetEmployees()
            };

            // Create invoicerule
            InvoiceRule invoiceRule = new InvoiceRule()
            {
                Car = car,
                Invoice = invoice,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            _db.InvoiceRules.Add(invoiceRule);
            await _db.SaveChangesAsync();
            return 1;
        }

        public List<InvoiceRule> GetUserHiredCars()
        {
            return (from invoiceRule in _db.InvoiceRules
                    join car in _db.Cars on invoiceRule.Car.LicensePlate equals car.LicensePlate
                    join invoice in _db.Invoices on invoiceRule.Invoice.Number equals invoice.Number
                    where invoice.Customer.Id == _userService.GetUserId()
                    select new InvoiceRule
                    {
                        Id = invoiceRule.Id,
                        Car = car,
                        Invoice = invoice,
                        StartDate = invoiceRule.StartDate,
                        EndDate = invoiceRule.EndDate
                    }).ToList();
        }

        public List<InvoiceRule> GetHiredCars()
        {
            return (from invoiceRule in _db.InvoiceRules
                    join car in _db.Cars on invoiceRule.Car.LicensePlate equals car.LicensePlate
                    join invoice in _db.Invoices on invoiceRule.Invoice.Number equals invoice.Number
                    select new InvoiceRule
                    {
                        Id = invoiceRule.Id,
                        Car = car,
                        Invoice = invoice,
                        StartDate = invoiceRule.StartDate,
                        EndDate = invoiceRule.EndDate
                    }).ToList();
        }
    }
}
