using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstAPI.Model;

namespace FirstAPI.Service
{
    public interface ICarService
    {
        Task<bool> AddCar(Car car);
        Task<bool> Delete(Car car);
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetById(int id);
        Task<bool> Update(Car car);
    }
}
