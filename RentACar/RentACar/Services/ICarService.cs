using RentACar.Models;
using RentACar.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface ICarService
    {
        public Task<int> AddCar(CarViewModel model);

        public List<Car> GetCars();

        public Task<int> RemoveCar(string licensePlate);
    }
}
