using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Sample.API.Controllers;
using Sample.API.Services;
using Sample.Contract.SwapiModel;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Test.UnitTests.SwapiPeole
{
    public class SwapiPeopleControllerTest : ApiClientBase
    {
        private readonly Fixture _fixture;
        private readonly Mock<ISwapiPeopleService> _mockPeopleService;
        private readonly Mock<ILogger<SwapiPeopleController>> _logger;
        private readonly SwapiPeopleController _sut;

        public SwapiPeopleControllerTest(ITestOutputHelper output) : base(output)
        {
            _fixture = new Fixture();

            _logger = new Mock<ILogger<SwapiPeopleController>>();
            _mockPeopleService = new Mock<ISwapiPeopleService>();
            _sut = new SwapiPeopleController(_logger.Object, _mockPeopleService.Object);
        }

        [Fact]
        public async Task GetPeople_SuccessAsync()
        {
            // Arrange
            var id = "1";
            var expectedData = _fixture.Create<People>();//FakeDataMother.SimplePeople();
            _mockPeopleService
                .Setup(x => x.GetPeopleAsync(id)).ReturnsAsync(expectedData);

            // Act
            var result = await _sut.GetAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result);

            string valueAsJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            OutputMessage(valueAsJson);
        }

        [Fact]
        public async Task GetAllPeople_SuccessAsync()
        {
            // Arrange
            var expectedData = _fixture.CreateMany<SwapiEntityList<People>>();//FakeDataMother.SimplePeopleList();
            _mockPeopleService
                .Setup(x => x.GetAllPeopleAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await _sut.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result);

            string valueAsJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            OutputMessage(valueAsJson);
        }

    }
}
