using System.Text.Json.Serialization;

namespace DadJokeApp.Server.Models
{
    public class SearchJoke
    {
        [JsonPropertyName("results")]
        required public List<Joke> Results { get; set; }

        [JsonPropertyName("search_term")]
        public string? SearchTerm { get; set; }
    }
}
