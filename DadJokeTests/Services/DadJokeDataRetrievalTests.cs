using DadJokeApp.Server.Models;
using DadJokeApp.Server.Services;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadJokeTests.Services
{
    public class DadJokeDataRetrievalTests
    {
        private static IHttpClientFactory _clientFactory;

        [Fact]
        public async Task GetRandomJokeAsyncShouldReturnTypeStringWhenSuccessful()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();
            
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("dad_service")).Returns(httpClient);

            var service = new DadJokeDataRetrieval(mockHttpClientFactory.Object);
            
            // Act
            var actualResponse = await service.GetRandomJokeAsync();
            // Assert
            Assert.IsType<string>(actualResponse);
        }

        [Fact]
        public async Task GetRandomJokeAsyncShouldReturnEmptyStringWhenHttpCallNotSuccessful()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage 
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("dad_service")).Returns(httpClient);

            var service = new DadJokeDataRetrieval(mockHttpClientFactory.Object);

            // Act
            var actualResponse = await service.GetRandomJokeAsync();
            // Assert
            Assert.Equal(string.Empty, actualResponse);
        }

        [Fact]
        public async Task GetDadJokeSearchAsyncShouldReturnNullWhenEmptyResponseObjectIsRecieved()
        {
            // Arrange
            var searchTerm = string.Empty;

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/search")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("dad_service")).Returns(httpClient);

            var service = new DadJokeDataRetrieval(mockHttpClientFactory.Object);

            // Act
            var actualResponse = await service.GetDadJokeSearchAsync(searchTerm);
            // Assert
            Assert.Null(actualResponse);
        }

        [Fact]
        public async Task GetDadJokeSearchAsyncShouldReturnNullWhenInvalidHttpResponseIsGiven()
        {
            // Arrange
            var searchTerm = "hipster";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/search")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("dad_service")).Returns(httpClient);

            var service = new DadJokeDataRetrieval(mockHttpClientFactory.Object);

            // Act
            var actualResponse = await service.GetDadJokeSearchAsync(searchTerm);
            // Assert
            Assert.Null(actualResponse);
        }

        [Fact]
        public async Task GetDadJokeSearchAsyncShouldReturnNullTypeSearchJokeWhenInvalidSearchTermIsGiven()
        {
            // Arrange
            var searchTerm = "Invalid Search Term";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/search")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("dad_service")).Returns(httpClient);

            var service = new DadJokeDataRetrieval(mockHttpClientFactory.Object);

            // Act
            var actualResponse = await service.GetDadJokeSearchAsync(searchTerm);
            // Assert
            Assert.Null(actualResponse);
        }

        [Fact]
        public async Task GetDadJokeSearchAsyncShouldReturnSearchJokeObjectWhenValidSearchTermIsGiven()
        {
            // Arrange
            var searchTerm = "hipster";

            var searchJoke = new SearchJoke
            {
                Results = new List<Joke>
                {
                    new Joke { JokeText = "This is a dad joke", JokeLength = JokeLength.Short },
                    new Joke { JokeText = "This is another dad joke", JokeLength = JokeLength.Short }
                },
            };

            var searchJokeJson = System.Text.Json.JsonSerializer.Serialize(searchJoke);

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage
            {
                Content = new StringContent(searchJokeJson)
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(result)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://icanhazdadjoke.com/search")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("dad_service")).Returns(httpClient);

            var service = new DadJokeDataRetrieval(mockHttpClientFactory.Object);

            // Act
            var actualResponse = await service.GetDadJokeSearchAsync(searchTerm);
            // Assert
            Assert.IsType<SearchJoke>(actualResponse);
        }
    }
}
