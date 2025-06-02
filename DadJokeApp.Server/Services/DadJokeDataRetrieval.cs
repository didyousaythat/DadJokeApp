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
        private readonly IHttpClientFactory _clientFactory;
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
            //Set up our client and create our variables
            var client = _clientFactory.CreateClient("dad_service");
            var jokeData = string.Empty;

            //clear our headers and set the Accept header to text/plain
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", acceptHeaderText);

            //make the request to the API and check if it was successful
            try
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    //read response content and check if it was empty or null before assigning and returning the value
                    var jokeDataString = await response.Content.ReadAsStringAsync();

                    jokeData = string.IsNullOrEmpty(jokeDataString) ? string.Empty : jokeDataString;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error retrieving dad joke from the API.", ex);
            }

            return jokeData;
        }

        /// <summary>
        /// Retreives a list of dad jokes from the ICanHazDadJoke API.
        /// </summary>
        /// <returns>A SearchJoke object</returns>
        public async Task<SearchJoke> GetDadJokeSearchAsync(string searchTerm)
        {
            //check that search term isn't more than one word
            if(searchTerm.Split(" ").Length > 1)
            {
                return null;
            }

            //set up our client and create our variables
            var client = _clientFactory.CreateClient("dad_service");
            SearchJoke jokeData = null;
            //check if search term is null or empty and set the search url accordingly
            var searchUrl = string.IsNullOrEmpty(searchTerm) ? apiUrl + "search?limit=30&term=" : apiUrl + "search?limit=30&term=" + searchTerm;

            //clear and set the headers for the request
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", acceptHeaderJson);

            //make the request to the API and check if it was successful
            try
            {
                var response = await client.GetAsync(searchUrl);

                if (response.IsSuccessStatusCode)
                {
                    //read the response content and deserialize it into a SearchJoke object
                    var jokeDataJsonString = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(jokeDataJsonString))
                    {
                        return null;
                    }

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
            }
            catch(HttpRequestException ex)
            {
                throw new HttpRequestException("Error retrieving dad jokes from the API.", ex);
            }
            return jokeData;
        }
    }
}
