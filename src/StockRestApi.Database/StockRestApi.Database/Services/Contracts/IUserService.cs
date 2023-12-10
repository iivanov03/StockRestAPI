namespace StockRestApi.Database.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string username, string email, string password);

        Task<bool> DoesUserExistAsync(string username, string password);
    }
}
