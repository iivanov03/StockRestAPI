using Microsoft.AspNetCore.Mvc;
using StockRestApi.Gateway.Model;
using StockRestApi.Gateway.Utils;
using Route = StockRestApi.Gateway.Model.Route;

namespace StockRestApi.Gateway.Controllers;

[ApiController]
[Route("/api")]
public class ProxyController : ControllerBase
{
    private IConfiguration _configuration;

    public ProxyController(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    [Route("/{*catchAll}")]
    public Object Proxy(string catchAll)
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
        // This may be unneeded when we implement the middleware, but for now we are doing it here.
        var route = UrlUtils.GetRouteSettings(cleanUrl, settingsRoutes);
        
        //TODO: Call the actual microservice.

        return route;
    }
}