using System.Net;

namespace StockRestApi.Gateway.Model;

public class GatewayException : Exception
{
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public GatewayException(string message, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}