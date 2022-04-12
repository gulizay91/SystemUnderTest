using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Sample.API.Controllers;
using Sample.API.Repositories;
using Sample.API.Services;
using Sample.DataGenerator;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Test.IntegrationTests.Movie
{
    public class MovieControllerTest : TestBase
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly Mock<ILogger<MovieController>> _logger;

        private readonly MovieController _sut;

        public MovieControllerTest(ITestOutputHelper output) : base(output)
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockMovieService = new Mock<IMovieService>();
            _logger = new Mock<ILogger<MovieController>>();

            _sut = new MovieController(_logger.Object, _mockMovieService.Object);
        }

        [Fact]
        public void GetMovie_Success()
        {
            // Arrange
            var movieDatas = MovieMother.SimpleMovieList(100);
            var random = new Random();
            int index = random.Next(1, 100);
            var rank = movieDatas[index].Rank;
            var expectedData = movieDatas.Find(r => r.Rank == rank);
            _mockMovieRepository.Setup(x => x.Movies).Returns(movieDatas).Verifiable();
            _mockMovieService.Setup(x => x.MovieRepo).Returns(_mockMovieRepository.Object);

            // Act
            var result = _sut.Get((int)rank);

            Assert.NotNull(result);
            Assert.Equal(rank, result.Rank);

            _mockMovieService.Verify(m => m.MovieRepo, Times.Once());
            _mockMovieService.Verify(m => m.GetMovies(), Times.Never());
            _mockMovieRepository.Verify(m => m.Movies, Times.Once());

            string valueAsJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            OutputMessage(valueAsJson);
        }

        [Fact]
        public void GetMovies_Success()
        {
            // Arrange
            var expectedData = MovieMother.SimpleMovieList(100);
            _mockMovieService.Setup(x => x.GetMovies()).Returns(expectedData).Verifiable();

            // Act
            var result = _sut.Get();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count() > 0);

            _mockMovieService.Verify(m => m.GetMovies(), Times.Once());

            string valueAsJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            //OutputMessage(valueAsJson);
        }

    }
}
