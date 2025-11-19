
using AppForSEII2526.API.DTOs.RentalDTOs;
using AppForSEII2526.API.DTOs.ReviewDTOs;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    public class ReviewsController: ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ApplicationDbContext context, ILogger<ReviewsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReviewDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetReviews(int id)
        {
            if (_context.Reviews == null)
            {
                _logger.LogError("Error:Reviews table does not exist");
                return NotFound();
            }

            var review = await _context.Reviews
    .Where(r => r.Id == id)
    .Include(r => r.ReviewItems)
        .ThenInclude(ri => ri.Car)
            .ThenInclude(car => car.Model)
    .Select(r => new ReviewDetailDTO(
        r.Id,
        r.ApplicationUser.UserName,
        r.Country,
        r.DriverType.ToString(),
        r.ReviewItems.Select(ri => new ReviewItemsDTO(
            ri.CarId,
            ri.Car.Model.Name,
            ri.Car.Color,
            ri.Car.FuelType,
            ri.Car.Manufacturer,
            ri.Rating,
            ri.Car.Description
        )).ToList())
    ).FirstOrDefaultAsync();

            if (review == null)
            {
                _logger.LogError($"Error: review with id {id} does not exist");
                return NotFound();
            }


            return Ok(review);
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReviewDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReview(ReviewForCreateDTO reviewForCreate)
        {

            if (reviewForCreate.ReviewItems == null || reviewForCreate.ReviewItems.Count == 0)
                ModelState.AddModelError("ReviewItems", "Error! Debes seleccionar al menos un coche para reseñar");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reviewForCreate.CustomerUserName);
            if (user == null)
                ModelState.AddModelError("ReviewApplicationUser", "Error! El nombre de usuario no está registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var carIds = reviewForCreate.ReviewItems.Select(ri => ri.CarID).ToList();

            var cars = _context.Cars
                .Include(c => c.ReviewItems)
                    .ThenInclude(ri => ri.Review)
                .Where(c => carIds.Contains(c.Id))
                .Select(c => new
                {
                   c.Id,
                   c.Model.Name,
                   c.FuelType,
                   c.Manufacturer,
                   c.Color,



                })
                 .ToList();

            var review = new Review(
                DateTime.Now,
               reviewForCreate.CustomerUserName,
               reviewForCreate.Country,
               reviewForCreate.DriverType,
                new List<ReviewItem>(),
                user


         );

               foreach (var item in reviewForCreate.ReviewItems)
            {
                var car = cars.FirstOrDefault(c => c.Id == item.CarID);
                if (car == null )
                {
                    ModelState.AddModelError("ReviewItems", $"Error! El coche '{item.Model}' no está disponible");
                }
                else if (item.Rating > 5 || item.Rating < 1)
                {
                    ModelState.AddModelError("ReviewItems", $"Error! Rating must be greater than 0 and smaller than 6");
                }
                else
                {
                    review.ReviewItems.Add(new ReviewItem(item.CarID,review,item.ReviewDescription,item.Rating)); //item.description
                    
                }
            }


            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Add(review);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Review", "Error al guardar la reseña, inténtelo de nuevo más tarde");
                return Conflict("Error: " + ex.Message);
            }

            var reviewDetail = new ReviewDetailDTO(
             
            );

            return CreatedAtAction("GetReview", new { id = review.Id }, reviewDetail);
        }

    }




}
