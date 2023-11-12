using StockRestApi.Database.Data.Enums;

namespace StockRestApi.Database.Data.Entities
{
    public class TransactionHistory : BaseEntity
    {
        public int StockPortfolioId { get; set; }
        public StockPortfolio StockPortfolio { get; set; }

        public decimal PricePerShare { get; set; }

        public double Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public TradeAction TradeAction { get; set; }
    }
}
