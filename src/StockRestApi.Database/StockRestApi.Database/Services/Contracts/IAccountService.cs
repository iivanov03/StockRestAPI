using StockRestApi.Database.Models.Account;

namespace StockRestApi.Database.Services.Contracts
{
    public interface IAccountService
    {
        Task<List<AccountModel>> GetAll();

        Task<AccountModel> GetByIdAsync(int id);

        Task<bool> CreateAsync(AccountBaseModel account);

        Task<bool> UpdateAsync(AccountModel account);

        Task<bool> DeleteAsync(int id);
    }
}
