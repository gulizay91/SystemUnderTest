using Microsoft.AspNetCore.Mvc;
using Sample.API.Services;
using Sample.Contract;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _service;

        public MovieController(ILogger<MovieController> logger, IMovieService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            _logger.LogInformation("GetMovie");
            return _service.GetMovies().ToArray();
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public Movie Get(int id)
        {
            _logger.LogInformation("GetMovie");
            var movie = _service.MovieRepo.Movies.Find(r => r.Rank == id);
            ArgumentNullException.ThrowIfNull(movie, nameof(movie));
            return movie;
        }
    }
}
