using StockRestApi.Database.Data.Entities;
using StockRestApi.Database.Models.Account;
using StockRestApi.Database.Repositories.Contracts;
using StockRestApi.Database.Services.Contracts;

namespace StockRestApi.Database.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        

        public async Task<AccountModel> GetByIdAsync(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            var model = new AccountModel()
            {
                Balance = account.Balance,
                UserId = account.UserId,
                Portfolio = null, // TODO
                Watchlist = null // TODO
            };

            return model;
        }

        public async Task<bool> CreateAsync(AccountBaseModel account)
        {
            var entity = new Account()
            {
                UserId = account.UserId,
                Balance = account.Balance
            };

            return await _accountRepository.CreateAsync(entity);
        }

        public async Task<bool> UpdateAsync(AccountModel model)
        {
            var account = new Account()
            {
                UserId = model.UserId,
                Balance = model.Balance
            };

            return await _accountRepository.UpdateAsync(account);
        }

        public async Task<bool> DeleteAsync(int id) => await _accountRepository.DeleteAsync(id);

        public async Task<List<AccountModel>> GetAll() => (await _accountRepository.GetAll())
            .Select(x => new AccountModel
            {
                Id = x.Id,
                Balance = x.Balance,
                UserId = x.UserId
            })
            .ToList();
    }
}