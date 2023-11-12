namespace StockRestApi.Database.Data.Entities
{
    public class StockHistory : BaseEntity
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}