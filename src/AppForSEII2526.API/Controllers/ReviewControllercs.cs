using AppForSEII2526.API.DTOs.ReviewDTOs;

namespace AppForSEII2526.API.Controllers
{
    public class ReviewsController
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
        )).ToList<ReviewItemsDTO>()


   ))
    .FirstOrDefaultAsync();

            if (review == null)
            {
                _logger.LogError($"Error: review with id {id} does not exist");
                return NotFound();
            }


            return Ok(review);
        }

        private ActionResult Ok(object review)
        {
            throw new NotImplementedException();
        }

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }
    }
}