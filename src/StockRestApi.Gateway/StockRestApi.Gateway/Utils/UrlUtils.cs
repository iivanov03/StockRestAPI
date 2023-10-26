using Cloudtoid.UrlPattern;

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
            catch (Exception)
            {
                // Just ignore, we don't need to do anything here...
            }

        }

        // If we got to here, this means that we did not find the needed route.
        throw new Exception("Could not find route");
    }

    public static string CleanUrl(string catchAll)
    {
        return catchAll.Replace("api/", "").Split("?=")[0];
    }
}