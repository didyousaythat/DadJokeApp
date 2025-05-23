using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DadJokeApp.Server.Controllers;
using DadJokeApp.Server.Models;
using DadJokeApp.Server.Services;
using Moq;

namespace DadJokeTests.Controllers
{
    public class DadJokeControllerTests
    {
        Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
        Mock<IDadJokeDataRetrieval> mockDadJokeDataRetrieval = new Mock<IDadJokeDataRetrieval>();

        [Fact]
        public void GetRandomJokeShouldReturnStringWhenNormalReturnIsSuccessful()
        {
            // Arrange
            var controller = new DadJokeController(mockHttpClientFactory.Object, mockDadJokeDataRetrieval.Object);
            // Act
            mockDadJokeDataRetrieval.Setup(x => x.GetRandomJokeAsync()).ReturnsAsync("This is a dad joke");

            var result = controller.GetRandomJoke();
            
            // Assert
            Assert.IsType<string>(result);
        }
    }
}
