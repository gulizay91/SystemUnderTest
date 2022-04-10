using Bogus;
using Sample.Contract;

namespace Sample.DataGenerator.BogusDataFaker
{
    public static class MovieDataFaker
    {
        /// <summary>
        /// Gets the MovieFaker.
        /// </summary>
        public static readonly Faker<Movie> MovieFaker = GenerateFaker();

        /// <summary>
        /// The GenerateFaker.
        /// </summary>
        /// <returns>The <see cref="Faker{Movie}"/>.</returns>
        public static Faker<Movie> GenerateFaker()
        {
            return new Faker<Movie>("tr")
                  .RuleFor(r => r.Id, r => Guid.NewGuid().ToString())
                  .RuleFor(r => r.Title, r => r.Person.FullName) // premium Bogus.Hollywood
                  .RuleFor(r => r.Rank, r => r.Random.Int(1, 100));
        }
    }
}
