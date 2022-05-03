using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FirstAPI.Model;
using Npgsql;

namespace FirstAPI.Data.Repositories
{

    public class CarRepository : ICarRepository
    {
        private PostgreSQLConfig _connectionString;

        public CarRepository(PostgreSQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> Add(Car car)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO cars(maker, model, color, year, doors)
                        VALUES(@Maker, @model, @Color, @Year, @Doors)";

            var result = await db.ExecuteAsync(sql, new { car.Maker, car.Model, car.Color, car.Year, car.Doors });
            return result > 0;
        }

        public async Task<bool> Delete(Car car)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE
                        FROM cars
                        WHERE id = @id";

            var result =  await db.ExecuteAsync(sql, new { Id = car.Id });
            return result > 0;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var db = dbConnection();
            var sql = @"
                        SELECT *
                        FROM cars";

            return await db.QueryAsync<Car>(sql, new { });
        }

        public async Task<Car> GetById(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cars
                        WHERE id = @id";

            return await db.QueryFirstOrDefaultAsync<Car>(sql, new { Id = id });
        }

        public async Task<bool> Update(Car car)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE cars
                        SET maker = @Maker,
                            model = @Model,
                            color = @Color,
                            year = @Year,
                            doors = @Doors
                        WHERE id = @Id;";

            var result = await db.ExecuteAsync(sql, new { car.Maker, car.Model, car.Color, car.Year, car.Doors, car.Id });
            return result > 0;
        }

        public async Task<bool> ExistsById(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cars
                        WHERE id = @id";

            var result = await db.QueryFirstOrDefaultAsync<Car>(sql, new { Id = id });

            if (result == null) return false;
            return true;
        }

        public async Task<bool> ExistsByModel(string model)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cars
                        WHERE model = @model";

            var result = await db.QueryFirstOrDefaultAsync<Car>(sql, new { Model = model });

            if (result == null) return false;
            return true;
        }
    }
}
