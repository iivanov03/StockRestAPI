namespace StockRestApi.Accounts.Models;
using StockRestApi.Accounts.Entities;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public double? Balance { get; set; }
    public string? Currency { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Balance = user.Balance;
        Token = token;
    }
}
