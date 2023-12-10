using System.ComponentModel.DataAnnotations;

namespace StockRestApi.Database.Models.Account
{
    public class AccountModel : AccountBaseModel
    {
        public int Id { get; set; }

        public IList<StockModel> Portfolio { get; set; }

        public IList<StockModel> Watchlist { get; set; }
    }

    public class StockModel
    {
        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal Price { get; set; }
    }
}
