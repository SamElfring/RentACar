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

            bool hasAccess = false;
            foreach (string role in roles)
            {
                if (role == "Admin" || role == "Employee")
                {
                    hasAccess = true;
                    break;
                }
            }

            ViewBag.CarClass = carClass;
            ViewBag.IsAdmin = hasAccess;
            ViewBag.Cars = _carService.GetCars(carClass);
            return View();
        }

        public IActionResult NewRent(string licensePlate = "none")
        {
            ViewBag.CarsDropdown = _carService.GetCarsFromSelectList();
            ViewBag.SelectedCar = licensePlate;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRent(InvoiceRuleViewModel model)
        {
            if (ModelState.IsValid)
            {
                int result = await _rentService.AddRent(model);

                if (result == 1)
                {
                    return RedirectToAction("MyRents", "Rent");
                }

                ModelState.AddModelError("", "Er is iets fout gegaan.");
            }
            return View();
        }

        public IActionResult MyRents()
        {
            ViewBag.Rents = _rentService.GetUserHiredCars();

            return View();
        }
    }
}
