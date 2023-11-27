using System.Net;
using Microsoft.Extensions.Primitives;

namespace StockRestApi.Gateway.Services;

public interface ProxyApiService
{
    public Task<string?> call(string url, HttpMethod method, Dictionary<string, StringValues> headers, string requestBody);
}