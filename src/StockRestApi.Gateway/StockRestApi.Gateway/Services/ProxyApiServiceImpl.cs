using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using StockRestApi.Gateway.Model;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace StockRestApi.Gateway.Services;

public class ProxyApiServiceImpl : ProxyApiService
{
    private readonly HttpClient _httpClient;

    public ProxyApiServiceImpl(HttpClient _httpClient)
    {
        this._httpClient = _httpClient;
    }

    private void addHeaders(Dictionary<string, StringValues> headerDictionary, HttpRequestMessage requestMessage)
    {
        foreach (var header in headerDictionary)
        {
            if (header.Key.ToLower() == "content-type")
            {
                
            }

            // Add without validation, as we don't really care what headers are sent here, we are only a proxy...
            requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }
    }

    public async Task<string?> call(string url, HttpMethod method, Dictionary<string, StringValues> headers,
        string requestBody)
    {
        var message = new HttpRequestMessage();
        message.Method = method;
        message.RequestUri = new Uri(url);
        this.addHeaders(headers, message);

        if (!requestBody.IsNullOrEmpty())
        {
            message.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        }
        
        var response = await _httpClient.SendAsync(message);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new GatewayException(errorMessage, response.StatusCode);
        }

        return await response.Content.ReadAsStringAsync();
    }
}