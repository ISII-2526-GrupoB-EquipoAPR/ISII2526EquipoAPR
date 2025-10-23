using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<CarsController> _logger;
        private char filtroColor;

        public CarsController(ApplicationDbContext context, ILogger<CarsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /*[HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ComputeDivision(decimal op1, decimal op2)
        {
            if (op2 == 0)
            {
                _logger.LogError($"{DateTime.Now} Exception: op2=0, division by 0");
                return BadRequest("op2 must be different from 0");
            }
            decimal result = decimal.Round(op1 / op2, 2);
            return Ok(result);
        }
        */
        /*
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Car>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCarsForPurchasing()
        {
            IList<Car> cars = await _context.Cars.ToListAsync();
            return Ok(cars);
        }
        */
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForReviewDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> GetCarsForPurchasingDTO()
        {
            var coches = await _context.Cars
                .Select(c => new CarForPurchaseDTO
                {
                    Id = c.Id,
                    Modelo = c.Model.Name,
                    Color = c.Color,
                    FuelType = c.FuelType,
                    Manufacturer = c.Manufacturer,
                    PurchasePrice = c.PurchasingPrice
                })
                .ToListAsync();
            return Ok(coches);

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForReviewDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> GetCarsForReviewDTO()
        {
            var coches = await _context.Cars
                .Where(c => c.Color.Contains(filtroColor) || (filtroColor == null))
                .Select(c => new CarForReviewDTO
                {
                    Id = c.Id,
                    Modelo = c.Model.Name,
                    Color = c.Color,
                    FuelType = c.FuelType,
                }).ToListAsync();
            return Ok(coches);

        }
    
