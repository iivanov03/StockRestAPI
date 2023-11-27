using System.Collections.Immutable;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using StockRestApi.Gateway.Model;
using StockRestApi.Gateway.Services;
using StockRestApi.Gateway.Utils;
using Route = StockRestApi.Gateway.Model.Route;

namespace StockRestApi.Gateway.Controllers;

[ApiController]
[Route("/api")]
public class ProxyController : ControllerBase
{
    private IConfiguration _configuration;
    private ProxyApiService _proxyApiService;

    public ProxyController(IConfiguration configuration, ProxyApiService proxyApiService)
    {
        this._configuration = configuration;
        this._proxyApiService = proxyApiService;
    }

    [Route("/{*catchAll}")]
    public async Task<Object> Proxy(string catchAll)
    {
        // We wil get the request url from the HttpContext, as we need some place that we can rely on to return the url in a constant format, So we can parse it properly.
        var url = HttpContext.Request.Path.ToUriComponent();
        // We will gather all of the routes here.
        var settingsRoutes = _configuration.GetSection("Routes").Get<List<Route>>();
        if (settingsRoutes == null)
        {
            throw new Exception("Could not find route settings.");
        }

        var cleanUrl = UrlUtils.CleanUrl(url);
        // We are doing this basically, so we don't hit apis outside of our network.
        var route = UrlUtils.GetRouteSettings(cleanUrl, settingsRoutes);
        
        var request = HttpContext.Request;

        var reqStr = "";

        using (StreamReader reader
               = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
        {
            reqStr = await reader.ReadToEndAsync();
        }

        var microservice = UrlUtils.createMicroserviceUrl(route, url);
        
        var response = await _proxyApiService.call(
            microservice,
            new HttpMethod(HttpContext.Request.Method),
            Request.Headers.ToDictionary(a => a.Key, a => a.Value), 
            reqStr);

        if (response == null)
        {
            throw new GatewayException("Server error", HttpStatusCode.InternalServerError);
        }

        return response;
    }
}