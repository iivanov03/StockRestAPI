namespace StockRestApi.Auth.Services.Contracts
{
    public interface IAuthService
    {
        string GenerateToken(string username);
    }
}
