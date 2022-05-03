using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Data.Repositories;
using FirstAPI.Model;
using FirstAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarRepository carRepository, ICarService carService, ILogger<CarController> logger)
        {
            _carRepository = carRepository;
            _carService = carService;
            _logger = logger;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task<IActionResult>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Accessing endpoint GetAll() - GET /api/car/", LogLevel.Information);

            var result = await _carService.GetAll();

            if (result.ToArray().Length == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        //[ProducesResponseType(typeof(Task<IActionResult>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Accessing endpoint Get() - GET /api/car/{id}", LogLevel.Information);

            if (!await _carRepository.ExistsById(id)) return NoContent();

            return Ok(await _carService.GetById(id));
        }

        [HttpPost]
        //[ProducesResponseType(typeof(Task<IActionResult>), (int) HttpStatusCode.OK)]
        //[ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] Car car)
        {
            _logger.LogInformation("Accessing endpoint Post() - POST /api/car/", LogLevel.Information);

            if (car == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _carRepository.ExistsByModel(car.Model))
                return BadRequest($"The model {car.Model} already exists");

            var created = await _carService.AddCar(car);
            return Created("created", created);
        }

        [HttpPut]
        //[ProducesResponseType(typeof(Task<IActionResult>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] Car car)
        {
            _logger.LogInformation($"Accessing endpoint PUT() - PUT /api/car/ : id {car.Id}", LogLevel.Information);

            if (car == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _carService.Update(car);
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Accessing endpoint Delete() - DELETE /api/car/{id}", LogLevel.Information);

            if (!await _carRepository.ExistsById(id))
                return NoContent();

            await _carService.Delete(new Car { Id = id });
            return Ok();
        }
    }
}
