using Microsoft.AspNetCore.Mvc;
using StockRestApi.Gateway.Model;
using StockRestApi.Gateway.Utils;
using Route = StockRestApi.Gateway.Model.Route;

namespace StockRestApi.Gateway.Controllers;

[ApiController]
[Route("/api")]
public class ProxyController
{
    private IConfiguration _configuration;

    public ProxyController(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    [Route("/{**catchAll}")]
    public Object Proxy(string catchAll)
    {
        // We will gather all of the routes here.
        var settingsRoutes = _configuration.GetSection("Routes").Get<List<Route>>();
        if (settingsRoutes == null)
        {
            throw new Exception("Could not find route settings.");
        }

        var cleanUrl = UrlUtils.CleanUrl(catchAll);
        
        // We are doing this basically, so we don't hit apis outside of our network.
        // This may be unneeded when we implement the middleware, but for now we are doing it here.
        var route = UrlUtils.GetRouteSettings(cleanUrl, settingsRoutes);

        return route;
    }
}