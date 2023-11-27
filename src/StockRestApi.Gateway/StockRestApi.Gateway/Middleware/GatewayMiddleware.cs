using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (routes == null)
            {
                throw new Exception("Could not find the Routes config.");
            }


            var route = UrlUtils.GetRouteSettings(path, routes);
            // We can have the same route listed different times, so we need to gather all the possible http methods for the route.
            var routeMethods = UrlUtils.getAllAvailableRouteMethods(route, routes);
            // Validate that we are calling the proper http method.
            ValidateHttpMethod(context.Request.Method, routeMethods);
            ValidateJwtToken(route.Access, token);
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
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
            await context.Response.CompleteAsync();
        }
    }

    private void ValidateJwtToken(Access access, string? token)
    {
        if (access == Access.Private)
        {
            if (token == null)
            {
                throw new GatewayException("Unauthorized", HttpStatusCode.Unauthorized);
            }

            var configSecret = _configuration["JWT_SECRET"];
            if (configSecret == null)
            {
                throw new GatewayException("Internal server error", HttpStatusCode.InternalServerError);
            }

            var jwtKey = Encoding.ASCII.GetBytes(configSecret);
            var validator = new JwtSecurityTokenHandler();
            try
            {
                validator.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                throw new GatewayException("Unauthorized", HttpStatusCode.Unauthorized);
            }
        }
    }

    private void ValidateHttpMethod(string method, List<string> allowedMethods)
    {
        var found = false;
        foreach (var allowedMethod in allowedMethods)
        {
            if (method != allowedMethod)
            {
                found = true;
            }
        }

        if (!found)
        {
            throw new GatewayException("Method not allowed", HttpStatusCode.MethodNotAllowed);
        }
    }
}