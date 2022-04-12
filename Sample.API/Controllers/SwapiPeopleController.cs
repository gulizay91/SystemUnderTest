using Microsoft.AspNetCore.Mvc;
using Sample.API.Services;
using Sample.Contract.SwapiModel;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiPeopleController : ControllerBase
    {
        private readonly ILogger<SwapiPeopleController> _logger;
        private readonly ISwapiPeopleService _service;

        public SwapiPeopleController(ILogger<SwapiPeopleController> logger, ISwapiPeopleService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<SwapiEntityList<People>>> GetAsync()
        {
            _logger.LogInformation("GetSwapiPeople");
            return await _service.GetAllPeopleAsync();
        }

        [HttpGet("{id}", Name = "GetPeople")]
        public async Task<People> GetAsync(string id)
        {
            _logger.LogInformation("GetPeople");
            return await _service.GetPeopleAsync(id);
        }

    }
}
