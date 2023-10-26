namespace StockRestApi.Gateway.Model;

public class Route
{
    public string Url { get; set; }
    public List<string> Method { get; set; }
    public string Access { get; set; }
}