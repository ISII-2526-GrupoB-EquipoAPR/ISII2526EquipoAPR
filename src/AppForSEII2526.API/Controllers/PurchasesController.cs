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
                    p.CustomerNameSurname, (PaymentMethodTypes)p.PaymentMethod, p.DeliveryAddress, p.PurchasingDate, p.PurchaseItems
                        .Select(pi => new PurchaseItemDTO(pi.Car.Id, pi.Car.Model.Name, pi.Car.Color, pi.Car.PurchasingPrice, pi.Quantity, pi.Car.Description)).ToList<PurchaseItemDTO>()))
             .FirstOrDefaultAsync();


            if (purchase == null)
            {
                _logger.LogError($"Error: Purchase with id {id} does not exist");
                return NotFound();
            }


            return Ok(purchase);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(RentalDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreatePurchase(PurchaseForCreateDTO purchaseForCreate)
        {
            if (purchaseForCreate.PurchaseItems.Count == 0)
            {
                ModelState.AddModelError("PurchaseItems", "Error! You must include at least one car to be purchased");
            }

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == purchaseForCreate.CustomerUserName);
            if (user == null)
            {
                ModelState.AddModelError("PurchaseApplicationUser", "Error! UserName is not registered");
            }
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var carIds = purchaseForCreate.PurchaseItems.Select(pi => pi.CarID).ToList();

            var cars = _context.Cars.Include(c => c.PurchaseItems).ThenInclude(pi => pi.Purchase)
                .Where(c => carIds.Contains(c.Id))
                .Select(c => new
                {
                    c.Id,
                    c.Model,
                    c.QuantityForPurchasing,
                    c.PurchasingPrice,

                    NumberOfPurchasedItems = c.PurchaseItems.Sum(pi => pi.Quantity)
                })
                .ToList();

            Purchase purchase = new Purchase(
                purchaseForCreate.CustomerUserName,
                purchaseForCreate.CustomerNameSurname,
                purchaseForCreate.DeliveryAddress,
                "",
                purchaseForCreate.PaymentMethod,
                DateTime.Now,
                new List<PurchaseItem>(),
                user
            );

            foreach (var item in purchaseForCreate.PurchaseItems)
            {
                var car = cars.FirstOrDefault(c => c.Id == item.CarID);

                if (car == null)
                {
                    ModelState.AddModelError("PurchaseItems", $"Error! Car with id {item.CarID} does not exist");
                }

                else if (item.Quantity > (car.QuantityForPurchasing - car.NumberOfPurchasedItems))
                {
                    ModelState.AddModelError("PurchaseItems", $"Error! Car '{car.Model}' does not have enough stock. Available: {car.QuantityForPurchasing - car.NumberOfPurchasedItems}, Requested: {item.Quantity}");
                }
                else
                {
                    purchase.PurchaseItems.Add(new PurchaseItem(
                        car.Id, purchase, car.PurchasingPrice, item.Quantity));
                    item.PurchasingPrice = car.PurchasingPrice;

                }
            }

            purchase.PurchasingPrice = purchase.PurchaseItems.Sum(pi => pi.Price * pi.Quantity);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Purchases.Add(purchase);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Purchase", $"Error! There was an error while saving your purchase, please try again later");
                return Conflict("Error: " + ex.Message);

            }
            var purchaseDetail = new PurchaseDetailDTO(
                purchase.Id,
                purchase.CustomerUserName,
                purchase.CustomerNameSurname,
                purchase.PaymentMethod,
                purchase.DeliveryAddress,
                purchase.PurchasingDate,
                purchaseForCreate.PurchaseItems
            );

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchaseDetail);



        }
    }

}

