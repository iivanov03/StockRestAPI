using Microsoft.AspNetCore.Identity;

namespace StockRestApi.Database.Data.Entities
{
    public class Account : BaseEntity
    {
        public Account()
        {
            StockPortfolio = new HashSet<StockPortfolio>();
            StockWatchlist = new HashSet<StockWatchlist>();
        }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public decimal Balance { get; set; }

        public ICollection<StockPortfolio> StockPortfolio { get; set; }

        public ICollection<StockWatchlist> StockWatchlist { get; set; }
    }
}
