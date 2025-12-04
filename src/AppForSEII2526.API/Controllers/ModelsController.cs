using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public ModelsController(ApplicationDbContext context, ILogger<ModelsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Models/GetModelsForPurchase
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetModels (string? modelName)
        {

            IList<string> models = await _context.Models
                .Where(model => (modelName == null || model.Name.Contains(modelName))) // where clause             
                .OrderBy(model => model.Name)
                .Select(model => model.Name)
                .ToListAsync();

            return Ok(models);
        }
    }
}