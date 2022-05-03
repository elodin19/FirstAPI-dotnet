using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstAPI.Data.Repositories;
using FirstAPI.Model;

namespace FirstAPI.Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository) => _carRepository = carRepository;

        public async Task<bool> AddCar(Car car)
        {
            return await _carRepository.Add(car);
        }

        public async Task<bool> Delete(Car car)
        {
            return await _carRepository.Delete(car);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _carRepository.GetAll();
        }

        public async Task<Car> GetById(int id)
        {
            return await _carRepository.GetById(id);
        }

        public async Task<bool> Update(Car car)
        {
            var car1 = await _carRepository.GetById(car.Id);

            if (await _carRepository.ExistsByModel(car.Model) && car1.Model != car.Model)
                return false;
            
            return await _carRepository.Update(car);
        }
    }
}
