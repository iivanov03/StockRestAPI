using System.Text.Json.Serialization;

namespace StockRestApi.Gateway.Model;

public class Route
{
    public string Url { get; set; }
    public List<string> Methods { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Access Access { get; set; }

    public string Host { get; set; }
}