using AutoFixture;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Sample.API.Clients;
using Sample.Contract.SwapiModel;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Test.IntegrationTests.Swapi
{
    public class SwapiClientTest : ApiClientBase
    {
        private readonly Fixture _fixture;
        private readonly HttpClient _httpClient;
        private readonly SWAPIClient _sut;

        public SwapiClientTest(ITestOutputHelper output) : base(output)
        {
            _fixture = new Fixture();
            _httpClient = new HttpClient(_messageHandlerMock.Object) { BaseAddress = new Uri(SWAPIClient.BaseAddress) };
            _sut = new SWAPIClient(_httpClient);
        }

        [Fact]
        public async Task GetPeople_From_SWAPIClient_SuccessAsync()
        {
            // Arrange
            var url = "people/1";
            var expectedUri = new Uri($"{SWAPIClient.BaseAddress}{url}");
            People expectedData = _fixture.Create<People>();//FakeDataMother.SimplePeople();

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedData))
            };

            _messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage).Verifiable();

            // Act
            var result = await _sut.GetAsync<People>(url);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData.name, result.name);

            // also check the 'http' call was like we expected it
            _messageHandlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get  // we expected a GET request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );

            //string valueAsJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented
            //});
            //OutputMessage(valueAsJson);
        }

    }
}
