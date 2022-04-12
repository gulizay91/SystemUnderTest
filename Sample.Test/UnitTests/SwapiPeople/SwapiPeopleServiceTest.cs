using AutoFixture;
using Moq;
using Newtonsoft.Json;
using Sample.API.Clients;
using Sample.API.Services;
using Sample.Contract.SwapiModel;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Test.UnitTests.SwapiPeole
{
    public class SwapiPeopleServiceTest : ApiClientBase
    {
        private readonly Fixture _fixture;
        private readonly Mock<ISWAPIPeopleClient> _mockPeopleClient;
        private readonly SwapiPeopleService _sut;

        public SwapiPeopleServiceTest(ITestOutputHelper output) : base(output)
        {
            _fixture = new Fixture();

            _mockPeopleClient = new Mock<ISWAPIPeopleClient>();
            _sut = new SwapiPeopleService(_mockPeopleClient.Object);
        }

        [Fact]
        public async Task GetPeople_SuccessAsync()
        {
            // Arrange
            var id = "1";
            var expectedData = _fixture.Create<People>();//FakeDataMother.SimplePeople();
            _mockPeopleClient
                .Setup(x => x.GetPeopleAsync(id)).ReturnsAsync(expectedData);

            // Act
            var result = await _sut.GetPeopleAsync(id);

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
            _mockPeopleClient
                .Setup(x => x.GetAllPeopleAsync()).ReturnsAsync(expectedData);

            // Act
            var result = await _sut.GetAllPeopleAsync();

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
