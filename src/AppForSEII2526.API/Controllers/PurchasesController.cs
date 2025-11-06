using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.DTOs.RentalDTOs;
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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetPurchase(int id)
        {
            if (_context.Purchases == null)
            {
                _logger.LogError("Error: Purchases table does not exist");
                return NotFound();
            }

            var purchase = await _context.Purchases
             .Where(p => p.Id == id)
                 .Include(p => p.PurchaseItems) //join table PurchaseItems
                    .ThenInclude(pi => pi.Car) //then join table Car
                        .ThenInclude(car => car.Model) //then join table Model
             .Select(p => new PurchaseDetailDTO(p.Id, p.CustomerUserName,
                    p.CustomerNameSurname, (PaymentMethodTypes)p.PaymentMethod, p.DeliveryAddress, p.PurchaseItems
                        .Select(pi => new PurchaseItemDTO(pi.Car.Id, pi.Car.Model.Name, pi.Car.Color, pi.Car.PurchasingPrice, pi.Quantity, pi.Car.Description)).ToList<PurchaseItemDTO>()))
             .FirstOrDefaultAsync();


            if (purchase == null)
            {
                _logger.LogError($"Error: Purchase with id {id} does not exist");
                return NotFound();
            }


            return Ok(purchase);
        }

        
    }

}

