using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
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

        public RentController(ICarService carService, IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
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

            ViewBag.IsAdmin = hasAccess;
            ViewBag.Cars = _carService.GetCars();
            return View();
        }
    }
}
