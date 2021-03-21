using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RugbyResults.Configuration;
using RugbyResults.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RugbyResults.Matches
{
    /// <summary>
    /// Class that contacts the external Match API
    /// </summary>
    public class MatchService : IMatchService
    {
        private IHttpClientFactory _clientFactory;
        private ExternalApiOptions _configuration;

        /// <summary>
        /// Initializes a new instance of the MatchService
        /// </summary>
        /// <param name="configuration">The external API configuration</param>
        /// <param name="clientFactory">The client factory</param>
        public MatchService(
            IOptions<ExternalApiOptions> configuration,
            IHttpClientFactory clientFactory)
        {
            _configuration = configuration.Value;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Gets the matches for a team by their ID
        /// </summary>
        /// <param name="teamId">The team ID</param>
        /// <returns></returns>
        public async Task<List<RugbyMatch>> GetByTeamId(int teamId)
        {
            HttpClient client = _clientFactory.CreateClient();

            string requestUri = $"{_configuration.Url}/v1/Match/List/{teamId}/{_configuration.Season}";

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri)
            };

            request.Headers.Add("AuthToken", _configuration.AuthToken);

            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"{response.StatusCode} - {errorMessage}");
            }
                
            using var responseStream = await response.Content.ReadAsStreamAsync();
            List<RugbyMatch> result = await JsonSerializer.DeserializeAsync<List<RugbyMatch>>(responseStream);

            return result;
        }
    }
    /// <summary>
    /// Defines the contract of the MatchService
    /// </summary>
    public interface IMatchService
    {
        Task<List<RugbyMatch>> GetByTeamId(int teamId);
    }
}
