using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models.ViewModels;
using RentACar.Services;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CarViewModel model)
        {
            if (ModelState.IsValid)
            {
                int result = await _carService.AddCar(model);

                if (result == 1)
                    return RedirectToAction("Index", "Rent");

                else if (result == 0)
                    ModelState.AddModelError("", "Deze auto bestaat al!");

                else
                    ModelState.AddModelError("", "Er is iets fout gegaan!");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromForm] string licensePlate)
        {
            await _carService.RemoveCar(licensePlate);
            return RedirectToAction("Index", "Rent");
        }
    }
}
