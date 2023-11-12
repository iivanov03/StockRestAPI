﻿using System.Text;

using Newtonsoft.Json;

using StockRestApi.Auth.Services.Contracts;

namespace StockRestApi.Auth.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("ApiClient");
            _client.BaseAddress = new Uri("http://api:80"); // TODO: Change url
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _client.GetAsync(endpoint);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T content)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            return await _client.PostAsync(endpoint, jsonContent);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T content)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            return await _client.PutAsync(endpoint, jsonContent);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            return await _client.DeleteAsync(endpoint);
        }
    }
}
