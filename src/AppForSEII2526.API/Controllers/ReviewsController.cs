using AppForSEII2526.API.DTOs.ReviewDTOs;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            try
            {
                if (_context.Reviews==null)
                {
                    _logger.LogError("Error:Reviews table does not exist");
                    return NotFound();
                }

                var review = await _context.Reviews
                            .Where(r => r.Id == id)
                            .Include(r => r.ReviewItems)
                                .ThenInclude(ri => ri.Car)
                                    .ThenInclude(car => car.Model)
                            .Include(r => r.ApplicationUser) 
                            .Select(r => new ReviewDetailDTO(
                                r.Id,
                                r.ApplicationUser.Name,  
                                r.Country,                    
                                r.DriverType, 
                                r.ReviewItems.Select(ri => new ReviewItemsDTO(
                                    ri.CarId,
                                    ri.Car.Model.Name,
                                    ri.Car.Color,
                                    ri.Car.FuelType,
                                    ri.Car.Manufacturer,
                                  ri.Car.Description
                                )).ToList<ReviewItemsDTO>())
                            ).FirstOrDefaultAsync();

                if (review == null)
                {
                    _logger.LogError($"Error: review with id {id} does not exist");
                    return NotFound();
                }

                _logger.LogInformation($"Review with id {id} obtained successfully");
                return Ok(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReviewDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReview(ReviewForCreateDTO reviewForCreate)
        {

            if ( reviewForCreate.ReviewItems.Count == 0)
                ModelState.AddModelError("ReviewItems", "Error! Debes seleccionar al menos un coche para reseñar");

            var user = _context.ApplicationUsers.FirstOrDefault(au => au.UserName == reviewForCreate.CustomerUserName);
            if (user == null)
                ModelState.AddModelError("ReviewApplicationUser", "Error! El nombre de usuario no está registrado");

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var carIds = reviewForCreate.ReviewItems.Select(ri => ri.CarID).ToList();

            var cars = _context.Cars.Include(c => c.ReviewItems)
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

            Review   review = new Review(
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
                    review.ReviewItems.Add(new ReviewItem(car.Id,review,item.ReviewDescription,
                        item.Rating)); //item.description
                    
                }
            }


            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Reviews.Add(review);

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
             review.Id,
             review.CustomerUserName,
             review.Country,
             review.DriverType,
             reviewForCreate.ReviewItems
            );

            return CreatedAtAction("GetReview", new { id = review.Id }, reviewDetail);
        }

    }




}
