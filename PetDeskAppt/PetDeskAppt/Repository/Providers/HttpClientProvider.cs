using Newtonsoft.Json;
using PetDeskAppt.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetDeskAppt.Repository.Providers
{
    public class HttpClientProvider : IHttpClientProvider
    {

        public string BaseUrl { get; }

        HttpClient _httpClient { get; }


        public HttpClientProvider(string url)
        {
            BaseUrl = !string.IsNullOrWhiteSpace(url) ? url : throw new ArgumentNullException("Url cannot be null or empty");
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<T>> GetAll<T>(string url)
        {
            var data = await _httpClient.GetStringAsync($"{BaseUrl}{url}");

            var result = JsonConvert.DeserializeObject<List<T>>(data);

            return result;
        }

        public Task<T> Post<T, U>(string url, U request)
        {
            return default;
        }

        public Task<T> GetById<T>(string url, string id)
        {
            return default;
        }
    }   
}
