using System.Text.Json.Serialization;

namespace DadJokeApp.Server.Models
{
    /// <summary>
    /// Enum values for joke length.
    /// </summary>
    public enum JokeLength
    {
        Short,
        Medium,
        Long
    }

    /// <summary>
    /// Joke class object for induvidual jokes and their length catagory.
    /// </summary>
    public class Joke
    {
        [JsonPropertyName("joke")]
        public required string JokeText { get; set; }

        public JokeLength? JokeLength { get; set; }
    }
}
