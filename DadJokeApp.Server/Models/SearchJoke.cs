using System.Text.Json.Serialization;

namespace DadJokeApp.Server.Models
{
    /// <summary>
    /// Parent object for the Joke object that contains the list of jokes and the search term used.
    /// </summary>
    public class SearchJoke
    {
        [JsonPropertyName("results")]
        required public List<Joke> Results { get; set; }

        [JsonPropertyName("search_term")]
        public string? SearchTerm { get; set; }
    }
}
