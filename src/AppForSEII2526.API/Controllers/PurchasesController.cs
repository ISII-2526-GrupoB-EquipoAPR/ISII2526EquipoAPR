using AppForSEII2526.API.DTOs.PurchaseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PurchasesController> _logger;

        public PurchasesController(ApplicationDbContext context, ILogger<PurchasesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        /*
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PurchaseDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPurchase(int id)
        {
            if (_context.Purchases == null)
            {
                _logger.LogError("Error: Purchases table does not exist");
                return NotFound();
            }

            var rental = await _context.Purchases
             .Where(p => p.Id == id)
                 .Include(p => p.PurchaseItems) //join table PurchaseItems
                    .ThenInclude(pi => pi.Car) //then join table Car
                        .ThenInclude(car => car.Model) //then join table Model
             .Select(p => new PurchaseDetailDTO(p.Id, p.CustomerUserName,
                    p.CustomerNameSurname, p.PaymentMethod ,p.DeliveryAddress, p.PurchaseItems
                        .Select(pi => new PurchaseItemDTO(pi.Car.Id,
                                pi.Car.Color, pi.Car.Model.Name,
                                pi.Car.PurchasingPrice,pi.Car.QuantityForPurchasing, pi.Description)).ToList<PurchaseItemDTO>()))
             .FirstOrDefaultAsync();


            if (rental == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }


            return Ok(rental);
        }
        */
    }
      

}

