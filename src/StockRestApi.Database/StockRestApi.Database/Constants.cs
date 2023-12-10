using StockRestApi.Database.Data.Entities;

namespace StockRestApi.Database
{
    public static class Constants
    {
        public class DB
        {
            public static readonly Dictionary<string, string> TableNames = new()
            {
                { typeof(Account).Name, Accounts },
                { typeof(Stock).Name, Stocks },
                { typeof(StockHistory).Name, StockHistories },
                { typeof(StockPortfolio).Name, StockPortfolios },
                { typeof(StockWatchlist).Name, StockWatchlists },
                { typeof(TransactionHistory).Name, TransactionHistory }
            };

            public const string Accounts = "Accounts";
            public const string Stocks = "Stocks";
            public const string StockHistories = "StockHistories";
            public const string StockPortfolios = "StockPortfolios";
            public const string StockWatchlists = "StockWatchlists";
            public const string TransactionHistory = "TransactionHistory";
        }
    }
}
