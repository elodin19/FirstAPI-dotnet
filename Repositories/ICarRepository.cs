using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstAPI.Model;

namespace FirstAPI.Data.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetById(int id);
        Task<bool> Add(Car car);
        Task<bool> Update(Car car);
        Task<bool> Delete(Car car);
        Task<bool> ExistsById(int id);
        Task<bool> ExistsByModel(string model);
    }
}
