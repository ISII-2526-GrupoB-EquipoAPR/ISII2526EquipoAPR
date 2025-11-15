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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateRental(RentalForCreateDTO rentalForCreate)
        {
            if (rentalForCreate.StartDate <= DateTime.Today)
                ModelState.AddModelError("StartDate", "Error! La fecha de inicio del alquiler debe ser posterior a hoy");

            if (rentalForCreate.StartDate >= rentalForCreate.EndDate)
                ModelState.AddModelError("EndDate", "Error! La fecha de fin debe ser posterior a la fecha de inicio");

            if (rentalForCreate.RentalItems == null || rentalForCreate.RentalItems.Count == 0)
                ModelState.AddModelError("RentalItems", "Error! Debes seleccionar al menos un coche para alquilar");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == rentalForCreate.CustomerUserName);
            if (user == null)
                ModelState.AddModelError("CustomerUserName", "Error! El nombre de usuario no está registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var carIds = rentalForCreate.RentalItems.Select(ri => ri.CarId).ToList();

            var cars = _context.Cars
                .Include(c => c.RentalItems)
                    .ThenInclude(ri => ri.Rental)
                .Where(c => carIds.Contains(c.Id))
                .Select(c => new
                {
                    c.Id,
                    ModelName = c.Model.Name,
                    c.QuantityForRenting,
                    c.RentingPrice,
                    NumberOfRentedItems = c.RentalItems
                        .Where(ri => ri.Rental.StartDate <= rentalForCreate.EndDate &&
                                     ri.Rental.EndDate >= rentalForCreate.StartDate)
                        .Sum(ri => ri.Quantity)
                })
                .ToList();

            var rental = new Rental(
                 rentalForCreate.DeliveryAddress,
                 rentalForCreate.CustomerUserName,
                 rentalForCreate.CustomerNameSurname,
                 rentalForCreate.DeliveryAddress, //no pasa nada  
                 rentalForCreate.EndDate,
                 rentalForCreate.StartDate,
                 DateTime.Now,
                 rentalForCreate.PaymentMethod,
                 new List<RentalItem>(),
                 user


            );

            rental.RentingPrice = 0;
            var numDays = (rental.EndDate - rental.StartDate).TotalDays;

            foreach (var item in rentalForCreate.RentalItems)
            {
                var car = cars.FirstOrDefault(c => c.Id == item.CarId);

                if (car == null)
                {
                    ModelState.AddModelError("RentalItems", $"Error! El coche con id {item.CarId} no existe");
                }
                else if (item.Quantity <= 0)
                {
                    ModelState.AddModelError("RentalItems", $"Error! La cantidad para el coche '{car.ModelName}' debe ser mayor que cero");
                }
                else if (item.Quantity > (car.QuantityForRenting - car.NumberOfRentedItems))
                {
                    ModelState.AddModelError("RentalItems", $"Error! El coche '{car.ModelName}' no tiene suficiente stock");
                }
                else if (car.NumberOfRentedItems >= car.QuantityForRenting) //no se si es necesario
                {                
                    ModelState.AddModelError("RentalItems", $"Error! El coche '{car.ModelName}' no está disponible entre {rentalForCreate.StartDate.ToShortDateString()} y {rentalForCreate.EndDate.ToShortDateString()}");
                }
                else
                {
                    rental.RentalItems.Add(new RentalItem(car.Id, rental,car.RentingPrice));
                    item.RentingPrice = car.RentingPrice;
                }
            }

            rental.RentingPrice = rental.RentalItems.Sum(ri => ri.PriceForRenting * (decimal)numDays);

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Add(rental);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Rental", "Error al guardar el alquiler, inténtelo de nuevo más tarde");
                return Conflict("Error: " + ex.Message);
            }

            var rentalDetail = new RentalDetailDTO(
              rental.Id,
              rental.CustomerUserName,
              rental.CustomerNameSurname,
              rental.DeliveryAddress,
              rentalForCreate.PaymentMethod,
              rental.StartDate,
              rental.EndDate,
              rental.RentingDate,
              rentalForCreate.RentalItems
            );

            return CreatedAtAction("GetRental", new { id = rental.Id }, rentalDetail);
        }

    }
}
