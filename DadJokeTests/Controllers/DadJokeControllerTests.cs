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
        Mock<IDadJokeDataRetrieval> mockDadJokeDataRetrieval = new Mock<IDadJokeDataRetrieval>();

        [Fact]
        public void GetRandomJokeShouldReturnStringWhenNormalReturnIsSuccessful()
        {
            // Arrange
            var controller = new DadJokeController(mockDadJokeDataRetrieval.Object);
            // Act
            mockDadJokeDataRetrieval.Setup(x => x.GetRandomJokeAsync()).ReturnsAsync("This is a dad joke");

            var result = controller.GetRandomJoke();
            
            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void GetRandomJokeSearchShouldReturnJokeSearchObjectWhenNormalReturnIsSuccessful()
        {
            // Arrange
            var controller = new DadJokeController(mockDadJokeDataRetrieval.Object);
            var searchTerm = "dad";

            SearchJoke searchJoke = new SearchJoke
            {
                Results = new List<Joke>
                {
                    new Joke { JokeText = "This is a dad joke", JokeLength = JokeLength.Short },
                    new Joke { JokeText = "This is another dad joke", JokeLength = JokeLength.Short }
                },
            };

            // Act
            mockDadJokeDataRetrieval.Setup(x => x.GetDadJokeSearchAsync(searchTerm)).ReturnsAsync(searchJoke);

            var result = controller.GetDadJokeSearch(searchTerm);

            // Assert
            Assert.IsType<SearchJoke>(result);
        }
    }
}
