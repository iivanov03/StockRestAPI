namespace StockRestApi.Database.Data.Entities
{
    public class StockPortfolio : BaseEntity
    {
        public StockPortfolio()
        {
            TransactionHistory = new HashSet<TransactionHistory>();
        }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public double Quantity { get; set; }

        public ICollection<TransactionHistory> TransactionHistory { get; set; }
    }
}