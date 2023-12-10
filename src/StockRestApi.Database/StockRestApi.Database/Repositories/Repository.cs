using StockRestApi.Database.Repositories.Contracts;

using Microsoft.EntityFrameworkCore;

using StockRestApi.Database.Data;

using static StockRestApi.Database.Constants.DB;

namespace StockRestApi.Database.Repositories
{

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        protected readonly string _tableName;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _tableName = TableNames[typeof(T).Name];
        }

        public virtual async Task<List<T>> GetAll()
        {
            string sql = $"SELECT * FROM {_tableName}";
            var entity = await _dbSet.FromSqlRaw(sql).ToListAsync();
            return entity;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE Id = {id}";
            var entity = await _dbSet.FromSqlRaw(sql).FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE Id = {id}";
            int result = await _dbContext.Database.ExecuteSqlRawAsync(sql);
            return result > 0;
        }

        public abstract Task<bool> CreateAsync(T entity);

        public abstract Task<bool> UpdateAsync(T entity);
    }
}
