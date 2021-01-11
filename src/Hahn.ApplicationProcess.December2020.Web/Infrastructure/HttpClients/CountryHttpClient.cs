using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.HttpClients
{
    public class CountryHttpClient : ICountryRepository
    {
        private readonly HttpClient _client;

        public CountryHttpClient(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> CheckCountryExistByNameAsync(string name)
        {
            var response = await _client.GetAsync($"rest/v2/name/{name}");
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonSerializer.Deserialize<List<ExpandoObject>>(content);
            if (result == null || result.Count == 0)
                return false;

            if (result[0].name.GetString() == name)
                return true;

            return false;
        }
    }
}