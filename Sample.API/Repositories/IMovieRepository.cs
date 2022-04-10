using Sample.Contract;

namespace Sample.API.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> Movies { get; }
    }
}
