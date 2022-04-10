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

namespace Sample.Test.IntegrationTests
{
    public class MovieTest : TestBase
    {
        private readonly Mock<IMovieRepository> mockMovieRepository;
        private readonly Mock<IMovieService> mockMovieService;
        private readonly Mock<ILogger<MovieController>> logger;

        private readonly MovieController _sutController;

        public MovieTest(ITestOutputHelper output) : base(output)
        {
            mockMovieRepository = new Mock<IMovieRepository>();
            mockMovieService = new Mock<IMovieService>();
            logger = new Mock<ILogger<MovieController>>();

            _sutController = new MovieController(logger.Object, mockMovieService.Object);
        }

        [Fact]
        public void GetMovie_Success()
        {
            // Arrange
            var movieDatas = FakeDataMother.SimpleMovieList(100);
            var random = new Random();
            int index = random.Next(1, 100);
            var rank = movieDatas[index].Rank;
            var expectedData = movieDatas.Find(r => r.Rank == rank);
            mockMovieRepository.Setup(x => x.Movies).Returns(movieDatas).Verifiable();
            mockMovieService.Setup(x => x.MovieRepo).Returns(mockMovieRepository.Object);

            // Act
            var result = _sutController.Get((int)rank);

            Assert.NotNull(result);
            Assert.Equal(rank, result.Rank);

            mockMovieService.Verify(m => m.MovieRepo, Times.Once());
            mockMovieService.Verify(m => m.GetMovies(), Times.Never());
            mockMovieRepository.Verify(m => m.Movies, Times.Once());

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
            var expectedData = FakeDataMother.SimpleMovieList(100);
            mockMovieService.Setup(x => x.GetMovies()).Returns(expectedData).Verifiable();

            // Act
            var result = _sutController.Get();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count() > 0);

            mockMovieService.Verify(m => m.GetMovies(), Times.Once());

            string valueAsJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            //OutputMessage(valueAsJson);
        }
    }
}
