using Moq;
using System.Net.Http;
using Xunit.Abstractions;

namespace Sample.Test
{
    public class ApiClientBase : TestBase
    {
        protected readonly Mock<HttpMessageHandler> _messageHandlerMock;
        public ApiClientBase(ITestOutputHelper output) : base(output)
        {
            _messageHandlerMock = new Mock<HttpMessageHandler>();
        }
    }
}
