using Domain.DeveloperNS;
using Domain.HttpService.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;


namespace Domain.HttpService
{
    public class MatchHttpService : IMatchHttpService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public MatchHttpService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IEnumerable<Developer>> GetMyMatches(Guid organizationUId)
        {
            var url = _config.GetSection("MatchAPI").Value.ToString() + "api/OrganizationMatch/my?organizationUId=" + organizationUId;
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<Developer>>();
                return json;
            }

            return default;
        }

        public async Task<IEnumerable<DeveloperDTO>> GetDevelopersToMatch(Guid organizationUId, int stackId)
        {
            var url = _config.GetSection("MatchAPI").Value.ToString() + "api/OrganizationMatch?organizationUId=" + organizationUId + "&stackId=" + stackId;
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<DeveloperDTO>>();
                return json;
            }

            return default;
        }

        public async Task<bool> MatchDeveloper(Guid developerUId, Guid organizationUId)
        {
            var url = _config.GetSection("MatchAPI").Value.ToString() + "api/OrganizationMatch";
            var match = new MatchNS.Match() { DeveloperUId = developerUId, OrganizationUId = organizationUId, Date = DateTime.UtcNow };
            var requestBody = JsonSerializer.Serialize(match);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            return default;
        }
    }
}
