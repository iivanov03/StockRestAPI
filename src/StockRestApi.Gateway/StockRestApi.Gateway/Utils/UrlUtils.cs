using System.Net;
using Cloudtoid.UrlPattern;
using StockRestApi.Gateway.Model;
using Route = StockRestApi.Gateway.Model.Route;

namespace StockRestApi.Gateway.Utils;

public class UrlUtils
{
    public static Model.Route GetRouteSettings(string url, List<Model.Route> routes)
    {
        var engine = new PatternEngine();
        foreach (var route in routes)
        {
            try
            {
                // Try if the string matches.
                // If we have successful match, then we can assume that this is the route, so we directly return it.
                engine.Match(route.Url, url);
                return route;
            }
            catch (Exception e)
            {
                // For logging purposes.
                Console.WriteLine(e);
                // Just ignore, we don't need to do anything here...
            }

        }

        // If we got to here, this means that we did not find the needed route.
        throw new GatewayException("Not found", HttpStatusCode.NotFound);
    }

    public static string CleanUrl(string catchAll)
    {
        // Remove the 'api' starting keyword from the url, and remove any query params, remove the ending trailing slash.
        return catchAll.Replace("/api", "").Split("?")[0].TrimEnd('/');
    }

    public static string createMicroserviceUrl(Route route, string url)
    {
        return $"http://{route.Host}{url}";
    }
}