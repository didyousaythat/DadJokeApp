using System.Text.Json;
using System.Text.Json.Serialization;
using DadJokeApp.Server.Models;

namespace DadJokeApp.Server.Services
{
    /// <summary>
    /// Helper class that retrieves dad jokes from the ICanHazDadJoke API using httpclient calls.
    /// </summary>
    public interface IDadJokeDataRetrieval
    {
        /// <summary>
        /// Retrieves a random dad joke from the ICanHazDadJoke API.
        /// </summary>
        /// <returns>A string containing the dad joke.</returns>
        public Task<string> GetRandomJokeAsync();

        /// <summary>
        /// Retreives a list of dad jokes from the ICanHazDadJoke API.
        /// </summary>
        /// <returns>A list of dad joke strings</returns>
        public Task<SearchJoke> GetDadJokeSearchAsync(string searchTerm);
    }
}
