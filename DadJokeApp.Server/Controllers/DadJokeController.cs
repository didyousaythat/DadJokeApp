using Microsoft.AspNetCore.Mvc;

namespace DadJokeApp.Server.Controllers
{
    [ApiController]
    [Route("dadjoke")] //will resolve route to /dadjoke
    public class DadJokeController : ControllerBase
    {
        private readonly ILogger<DadJokeController> _logger;
        public DadJokeController(ILogger<DadJokeController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public string GetRandomJoke()
        {
            // needs to be replaced with a real dad joke API call
            return "Why did the scarecrow win an award? Because he was outstanding in his field!";
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
