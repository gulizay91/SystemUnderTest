using Newtonsoft.Json;
using Sample.API.Repositories;
using Sample.Contract;

namespace Sample.API.Services
{
    
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        public MovieService(IMovieRepository repo)
        {
            _repo = repo;
        }

        public IMovieRepository MovieRepo => _repo??throw new NotImplementedException();

        public List<Movie> GetMovies()
        {
            return _repo.Movies;
        }
    }
}
