namespace StockRestApi.Accounts.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public double? Balance { get; set; }
    public string Tier { get; set; }

    [JsonIgnore]
    public string? Password { get; set; }
}