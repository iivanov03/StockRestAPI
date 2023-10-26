using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockRestApi.Gateway.Model;
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
       
        try
        {
            var path = UrlUtils.CleanUrl(context.Request.Path.ToUriComponent());
            var routes = this._configuration.GetSection("Routes").Get<List<Route>>();
            if (routes == null)
            {
                throw new Exception("Could not find the Routes config.");
            }

            var route = UrlUtils.GetRouteSettings(path, routes);
            // Validate that we are calling the proper http method.
            ValidateHttpMethod(context.Request.Method, route.Methods);
            // Call the next route.
            await next.Invoke(context);
        }
        catch (GatewayException exception)
        {
            context.Response.StatusCode = (int)exception.StatusCode;
            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
            await context.Response.CompleteAsync();
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
            await context.Response.CompleteAsync();
        }
    }

    private void ValidateHttpMethod(string method, List<string> allowedMethods)
    {
        foreach (var allowedMethod in allowedMethods)
        {
            if (method != allowedMethod)
            {
                throw new GatewayException("Method not allowed", HttpStatusCode.MethodNotAllowed);
            }
        }
    }
}