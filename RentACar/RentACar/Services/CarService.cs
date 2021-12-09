using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Models;
using RentACar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _db;

        public CarService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddCar(CarViewModel model)
        {
            if (model != null && _db.Cars.Any(x => x.LicensePlate == model.LicensePlate))
            {
                // Car already exists
                return 0;
            }
            else
            {
                Car car = new Car()
                {
                    LicensePlate = model.LicensePlate,
                    Brand = model.Brand,
                    Type = model.Type,
                    DayPrice = model.DayPrice,
                    Class = model.Class
                };
                _db.Cars.Add(car);
                await _db.SaveChangesAsync();

                return 1;
            }
        }

        public List<Car> GetCars(string sort)
        {
            sort = sort.ToLower();

            switch (sort)
            {
                case "lowclass":
                    return _db.Cars.Where(x => x.Class == "Low").ToList();
                case "midclass":
                    return _db.Cars.Where(x => x.Class == "Mid").ToList();
                case "highclass":
                    return _db.Cars.Where(x => x.Class == "High").ToList();
                case "price-lowhigh":
                    return _db.Cars.OrderBy(x => x.DayPrice).ToList();
                case "price-highlow":
                    return _db.Cars.OrderByDescending(x => x.DayPrice).ToList();
                default:
                    return _db.Cars.ToList();
            }
        }

        public async Task<int> RemoveCar(string licensePlate)
        {
            Car car = _db.Cars.Where(x => x.LicensePlate == licensePlate).FirstOrDefault();
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();

            return 1;
        }

        public List<SelectListItem> GetCarsFromSelectList()
        {
            var items = new List<SelectListItem>();
            List<Car> cars = _db.Cars.ToList();

            foreach (Car car in cars)
            {
                items.Add(new SelectListItem { Value = car.LicensePlate, Text = $"{car.Brand} {car.Type} - {car.LicensePlate}" });
            }
            return items.ToList();
        }

        public Car GetCarFromLicensePlate(string licensePlate)
        {
            return _db.Cars.Where(x => x.LicensePlate == licensePlate).FirstOrDefault();
        }
    }
}
