using System.Text.Json;
using System.Text.Json.Serialization;
using DadJokeApp.Server.Models;

namespace DadJokeApp.Server.Services
{
    /// <summary>
    /// Helper class that retrieves dad jokes from the ICanHazDadJoke API using httpclient calls.
    /// </summary>
    public class DadJokeDataRetrieval : IDadJokeDataRetrieval
    {
        private static IHttpClientFactory _clientFactory;
        private const string apiUrl = "https://icanhazdadjoke.com/";
        private const string acceptHeader = "application/json";

        public DadJokeDataRetrieval(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Retrieves a random dad joke from the ICanHazDadJoke API.
        /// </summary>
        /// <returns>A string containing the dad joke.</returns>
        public async Task<string> GetRandomJokeAsync()
        {
            var client = _clientFactory.CreateClient("dad_service");
            var jokeData = string.Empty;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", acceptHeader);

            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jokeDataJsonString = await response.Content.ReadAsStringAsync();
                jokeData = string.IsNullOrEmpty(jokeDataJsonString) ? string.Empty : JsonSerializer.Deserialize<Joke>(jokeDataJsonString).JokeText;
            }

            return jokeData;
        }
    }
}
