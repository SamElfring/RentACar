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
            List<Car> list = new List<Car>();

            switch (sort)
            {
                case "class":
                    list = _db.Cars.OrderBy(x => x.Class).ToList();
                    break;
                case "price-lowhigh":
                    list = _db.Cars.OrderByDescending(x => x.DayPrice).ToList();
                    break;
                default:
                    list = _db.Cars.ToList();
                    break;
            }

            return list;
        }

        public async Task<int> RemoveCar(string licensePlate)
        {
            Car car = _db.Cars.Where(x => x.LicensePlate == licensePlate).FirstOrDefault();
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();

            return 1;
        }
    }
}
