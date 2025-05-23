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
        private const string acceptHeaderJson = "application/json";
        private const string acceptHeaderText = "text/plain";

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
            client.DefaultRequestHeaders.Add("Accept", acceptHeaderText);

            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jokeDataString = await response.Content.ReadAsStringAsync();
                jokeData = string.IsNullOrEmpty(jokeDataString) ? string.Empty : jokeDataString;
            }

            return jokeData;
        }

        /// <summary>
        /// Retreives a list of dad jokes from the ICanHazDadJoke API.
        /// </summary>
        /// <returns>A SearchJoke object</returns>
        public async Task<SearchJoke> GetDadJokeSearchAsync(string searchTerm)
        { 
            //set up our client and create our variables
            var client = _clientFactory.CreateClient("dad_service");
            SearchJoke jokeData = null;
            var searchUrl = apiUrl + "search?limit=30&term=" + searchTerm;

            //clear and set the headers for the request
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", acceptHeaderJson);

            //make the request to the API and check if it was successful
            var response = await client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                //read the response content and deserialize it into a SearchJoke object
                var jokeDataJsonString = await response.Content.ReadAsStringAsync();
                jokeData = JsonSerializer.Deserialize<SearchJoke>(jokeDataJsonString);

                //loop through the list of jokes and set the JokeLength according to the amount of words in the string
                foreach (Joke joke in jokeData.Results)
                {
                    var wordCount = joke.JokeText.Split(' ').Length;

                    if (wordCount < 10)
                    {
                        joke.JokeLength = JokeLength.Short;
                    }
                    else if (wordCount >= 10 && wordCount < 20)
                    {
                        joke.JokeLength = JokeLength.Medium;
                    }
                    else
                    {
                        joke.JokeLength = JokeLength.Long;
                    }
                }

            }
            return jokeData;
        }
    }
}
