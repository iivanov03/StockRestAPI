namespace StockRestApi.Database.Data.Entities
{
    public class StockWatchlist : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}