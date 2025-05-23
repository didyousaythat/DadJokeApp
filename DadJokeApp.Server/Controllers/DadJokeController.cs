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
        private static IDadJokeDataRetrieval _jokeRetreiver;

        public DadJokeController(IDadJokeDataRetrieval jokeRetreiver)
        {
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
        public SearchJoke GetDadJokeSearch([FromQuery] string searchTerm = "")
        {  
            if(searchTerm == null)
            {
                return null;
            }

            return _jokeRetreiver.GetDadJokeSearchAsync(searchTerm).Result;
        }
    }
}
