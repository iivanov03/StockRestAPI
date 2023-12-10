using Microsoft.EntityFrameworkCore;

using StockRestApi.Database.Data;
using StockRestApi.Database.Data.Entities;
using StockRestApi.Database.Repositories.Contracts;

using static StockRestApi.Database.Constants;

namespace StockRestApi.Database.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }

        public override async Task<bool> CreateAsync(Account entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            string sql = @$"INSERT INTO {_tableName}
                            VALUES 
                            ({entity.UserId}, {entity.Balance})";

            int result = await _dbContext.Database.ExecuteSqlRawAsync(sql);
            return result > 0;
        }

        public override async Task<bool> UpdateAsync(Account entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            string sql = @$"UPDATE {_tableName} 
                            SET 
                                UserId = {entity.UserId},
                                Balance = {entity.Balance}
                            WHERE Id = {entity.Id}";

            int result = await _dbContext.Database.ExecuteSqlRawAsync(sql);
            return result > 0;
        }
    }
}
