using Microsoft.AspNetCore.Mvc;
using DadJokeApp.Server.Services;

namespace DadJokeApp.Server.Controllers
{
    /// <summary>
    /// Controller class for handling dad joke requests.
    /// </summary>
    [ApiController]
    [Route("dadjoke")]
    public class DadJokeController : ControllerBase
    {
        private static ILogger<DadJokeController> _logger;
        private static IHttpClientFactory _clientFactory;
        private static IDadJokeDataRetrieval _jokeRetreiver;

        public DadJokeController(IHttpClientFactory clientFactory, IDadJokeDataRetrieval jokeRetreiver)
        {
            _clientFactory = clientFactory;
            _jokeRetreiver = jokeRetreiver;
        }

        //DadJokeDataRetrieval jokeRetreiver = new DadJokeDataRetrieval(_clientFactory);


        [HttpGet]
        public string GetRandomJoke()
        {
            // will call the dad joke service class methods to grab a random dad joke
            
            return _jokeRetreiver.GetRandomJokeAsync().Result;
        }
    }


    /*
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }*/
}
