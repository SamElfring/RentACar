using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models.ViewModels;
using RentACar.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    public class RentController : Controller
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IRentService _rentService;

        public RentController(ICarService carService, IUserService userService, IRentService rentService)
        {
            _carService = carService;
            _userService = userService;
            _rentService = rentService;
        }

        [HttpGet("Rent/Index/{carClass?}")]
        [Authorize]
        public async Task<IActionResult> Index(string carClass = "none")
        {
            List<string> roles = await _userService.GetUserRoles();

            ViewBag.CarClass = carClass;
            ViewBag.IsAdmin = await _userService.HasAccess();
            ViewBag.Cars = _carService.GetCars(carClass);
            return View();
        }

        [Authorize]
        public IActionResult NewRent(string licensePlate = "none")
        {
            ViewBag.CarsDropdown = _carService.GetCarsFromSelectList();
            ViewBag.SelectedCar = licensePlate;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> NewRent(InvoiceRuleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate model
                (bool, string) checkDate = IsDateBeforeOrToday(model.StartDate.ToString());
                if (!checkDate.Item1)
                {
                    ModelState.AddModelError("StartDate", checkDate.Item2);
                    ViewBag.CarsDropdown = _carService.GetCarsFromSelectList();
                    ViewBag.SelectedCar = model.Car;
                    return View();
                }

                int result = await _rentService.AddRent(model);

                if (result == 1)
                {
                    return RedirectToAction("MyRents", "Rent");
                }

                ModelState.AddModelError("", "Er is iets fout gegaan.");
            }
            return View();
        }

        private static (bool, string) IsDateBeforeOrToday(string input)
        {
            DateTime inputTime;
            var parseResult = DateTime.TryParse(input, out inputTime);

            if (!parseResult)
                return (false, "Geen geldige datum!");

            if (inputTime <= DateTime.Now)
                return (false, "Kies een latere datum!");


            return (true, "");
        }

        [Authorize]
        public IActionResult MyRents()
        {
            ViewBag.Rents = _rentService.GetUserHiredCars();

            return View();
        }

        [Authorize(Roles = "Admin,Employee")]
        public IActionResult AllRents()
        {
            ViewBag.Rents = _rentService.GetHiredCars();

            return View();
        }
    }
}
