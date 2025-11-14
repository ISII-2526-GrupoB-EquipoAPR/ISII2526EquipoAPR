using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
        /*
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForPurchaseDTO>), (int)HttpStatusCode.OK)]

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
        */
        /*
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForPurchaseDTO>), (int)HttpStatusCode.OK)]
        
        public async Task<ActionResult> GetCarsForPurchasingDTO_Filter_Color(string? filtroColor)
        {
            var coches = await _context.Cars
                .Where(c => c.Color.Contains(filtroColor) || (filtroColor == null ) )
                .Select(c => new CarForPurchaseDTO

        */
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForPurchaseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCarForPurchase(string? carColor, string? carModel)
        {
            IList<CarForPurchaseDTO> selectCars = await _context.Cars

                .Include(c => c.Model)
                .Include(c => c.PurchaseItems).ThenInclude(pi => pi.Purchase)

                .Where(c => c.QuantityForPurchasing > 0 &&
                   (carColor == null || c.Color.Contains(carColor))
                    && (carModel == null || c.Model.Name.Equals(carModel))
                    )

                

                .Select(c => new CarForPurchaseDTO
                {
                    Id = c.Id,
                    Model = c.Model.Name,
                    Color = c.Color,
                    FuelType = c.FuelType,
                    Manufacturer = c.Manufacturer,
                    PurchasingPrice = c.PurchasingPrice
                })

                .ToListAsync();

            return Ok(selectCars);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForRentalDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCarForRental(string? model, decimal? rentingPrice)
        {


            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);


            IList<CarForRentalDTO> selectCars = await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.RentalItems).ThenInclude(ri => ri.Rental)
                .Where(c =>
                c.QuantityForRenting > 0 &&
                (model == null || c.Model.Name.Equals(model)) &&
                (rentingPrice == null || c.RentingPrice <= rentingPrice)&&
                 (c.RentalItems.Count(ri => ri.Rental.StartDate <= endDate
                                            && ri.Rental.EndDate >= startDate) < c.QuantityForRenting)

)
                .OrderBy(c => c.Color)
                .Select(c => new CarForRentalDTO
                {
                    Id = c.Id,
                    Model = c.Model.Name,
                    FuelType = c.FuelType,
                    Color = c.Color,
                    Manufacturer = c.Manufacturer,
                    PriceForRenting = c.RentingPrice
                })
                .ToListAsync();

            return Ok(selectCars);
        }



        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<CarForReviewDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetCarForReview(string? Manufacturer, string? Fueltype)
        {
            IList<CarForReviewDTO> selectCars = await _context.Cars
                .Include(c => c.Model)
                .Include(c => c.ReviewItems).ThenInclude(ri => ri.Review)
                .Where(c => (Manufacturer == null || c.Manufacturer.Contains(Manufacturer))
                    && (Fueltype == null || c.FuelType.Equals(Fueltype))
                    )
                .OrderBy(c => c.Model)
                .Select(c => new CarForReviewDTO
                {
                    Id = c.Id,
                    Modelo = c.Model.Name,
                    CarClass = c.CarClass,
                    Manufacturer = c.Manufacturer,
                    FuelType = c.FuelType,
                    Color = c.Color
                })
                .ToListAsync();





            return Ok(selectCars);
        }
    }
}
