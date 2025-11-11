using AppForSEII2526.API.DTOs.RentalDTOs;


namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(ApplicationDbContext context, ILogger<RentalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetRental(int id)
        {
            if (_context.Rentals == null)
            {
                _logger.LogError("Error: Rentals table does not exist");
                return NotFound();
            }

            var rental = await _context.Rentals
                .Where(r => r.Id == id)
                .Include(r => r.RentalItems)
                    .ThenInclude(ri => ri.Car)
                        .ThenInclude(car => car.Model)
                .Select(r => new RentalDetailDTO(
                    r.Id, r.CustomerUserName, r.CustomerNameSurname, r.DeliveryAddress,
                    (PaymentMethodTypes)r.PaymentMethod, r.StartDate, r.EndDate, r.RentingDate,r.RentalItems
                    .Select(ri => new RentalItemDTO(ri.Car.Id, ri.Car.Model.Name, ri.Car.Manufacturer, ri.Car.RentingPrice, ri.Quantity
                )).ToList<RentalItemDTO>()
                ))
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }

            return Ok(rental);
        }
    }
}
