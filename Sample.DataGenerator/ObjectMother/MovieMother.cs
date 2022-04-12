using Sample.Contract;
using Sample.DataGenerator.BogusDataFaker;

namespace Sample.DataGenerator
{
    public static class MovieMother
    {

        /// <summary>
        /// The SimpleMovie.
        /// </summary>
        /// <returns>The <see cref="Movie"/>.</returns>
        public static Movie SimpleMovie()
        {
            return MovieDataFaker.MovieFaker.Generate();
        }

        /// <summary>
        /// The SimpleMovieList.
        /// </summary>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Movie}"/>.</returns>
        public static List<Movie> SimpleMovieList(int count)
        {
            return MovieDataFaker.MovieFaker.Generate(count);
        }
    }
}