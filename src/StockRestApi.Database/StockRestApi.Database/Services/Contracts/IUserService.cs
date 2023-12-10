namespace StockRestApi.Database.Services.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterAsync(string username, string email, string password);

        Task<string> GetUserId(string username, string password);
    }
}
