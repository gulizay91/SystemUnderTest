using Newtonsoft.Json;
using Sample.Contract;

namespace Sample.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public List<Movie> Movies => _data;

        private static readonly List<Movie> _data = GetDataFromJson();

        private static List<Movie> GetDataFromJson()
        {
            // read file into a string and deserialize JSON to a type
            //Movie movie1 = JsonConvert.DeserializeObject<Movie>(System.IO.File.ReadAllText(@"c:\movie.json"));
            // deserialize JSON directly from a file
            //using (StreamReader file = System.IO.File.OpenText(@"c:\movie.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
            //}
            return JsonConvert.DeserializeObject<List<Movie>>(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}etc/data/movie.json"))!;
        }
    }
}
