using Microsoft.AspNetCore.Mvc;
using DadJokeApp.Server.Services;
using DadJokeApp.Server.Models;

namespace DadJokeApp.Server.Controllers
{
    /// <summary>
    /// Controller class for handling dad joke requests.
    /// </summary>
    [ApiController]
    public class DadJokeController : ControllerBase
    {
        //private static ILogger<DadJokeController> _logger;
        private static IHttpClientFactory _clientFactory;
        private static IDadJokeDataRetrieval _jokeRetreiver;

        public DadJokeController(IHttpClientFactory clientFactory, IDadJokeDataRetrieval jokeRetreiver)
        {
            _clientFactory = clientFactory;
            _jokeRetreiver = jokeRetreiver;
        }


        [HttpGet]
        [Route("dadjoke")]
        public string GetRandomJoke()
        {   
            return _jokeRetreiver.GetRandomJokeAsync().Result;
        }

        [HttpGet]
        [Route("dadjoke/search")]
        public SearchJoke GetDadJokeSearch(string searchTerm)
        {  
            return _jokeRetreiver.GetDadJokeSearchAsync(searchTerm).Result;
        }
    }
}
