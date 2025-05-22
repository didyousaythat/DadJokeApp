using System.Text.Json.Serialization;

namespace DadJokeApp.Server.Models
{
    public enum JokeLength
    {
        Short,
        Medium,
        Long
    }

    public class Joke
    {
        [JsonPropertyName("joke")]
        public required string JokeText { get; set; }

        public JokeLength? JokeLength { get; set; }
    }
}
