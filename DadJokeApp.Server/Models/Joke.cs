using System.Text.Json.Serialization;

namespace DadJokeApp.Server.Models
{
    public class Joke
    {
        public string Id { get; set; }
        [JsonPropertyName("joke")]
        public string JokeText { get; set; }
        public int Status { get; set; }
    }
}
