using StockRestApi.Gateway.Utils;
using Route = StockRestApi.Gateway.Model.Route;

namespace StockRestApi.Gateway.Middleware;

public class GatewayMiddleware : IMiddleware
{
    private IConfiguration _configuration;

    public GatewayMiddleware(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var path = UrlUtils.CleanUrl(context.Request.Path.ToUriComponent());
        var routes = this._configuration.GetSection("Routesc").Get<List<Route>>();
        if (routes == null)
        {
            throw new Exception("Hello world !");
        }

        var route = UrlUtils.GetRouteSettings(path, routes);
        // Call the next route.
        await next.Invoke(context);
    }
}