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
        private readonly IEmailSender _emailSender;

        public RentService(ApplicationDbContext db, ICarService carService, IUserService userService, IEmailSender emailSender)
        {
            _db = db;
            _userService = userService;
            _carService = carService;
            _emailSender = emailSender;
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

            var message = new Message(new string[] { await _userService.GetEmail() }, "Factuur - Rent A Car",
                "<div>" +
                    $"<h2>Factuur - {car.LicensePlate}</h2>" +
                    "<table>" +
                        "<tr style=\"background-color: green; color: white;\">" +
                            "<th style=\"padding: 10px;\">Auto</th>" +
                            "<th style=\"padding: 10px;\">Startdatum</th>" +
                            "<th style=\"padding: 10px;\">Einddatum</th>" +
                            "<th style=\"padding: 10px;\">Dagprijs</th>" +
                            "<th style=\"padding: 10px;\">Totaal</th>" +
                        "</tr>" +
                        "<tr>" +
                            $"<td style=\"padding: 10px;\">{car.Brand} {car.Type}</td>" +
                            $"<td style=\"padding: 10px;\">{model.StartDate}</td>" +
                            $"<td style=\"padding: 10px;\">{model.EndDate}</td>" +
                            $"<td style=\"padding: 10px;\">€{car.DayPrice}</td>" +
                            $"<td style=\"padding: 10px;\">€{(car.DayPrice * ((invoiceRule.EndDate - invoiceRule.StartDate).Days))}</td>" +
                        "</tr>" +
                    "</table>" +
                "</div>");
            _emailSender.SendEmail(message);

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
