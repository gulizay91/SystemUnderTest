using Sample.API.Repositories;
using Sample.Contract;

namespace Sample.API.Services
{
    public interface IMovieService
    {
        IMovieRepository MovieRepo { get; }
        List<Movie> GetMovies();
    }
}
