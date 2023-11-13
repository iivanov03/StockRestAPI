using System.ComponentModel.DataAnnotations;

namespace StockRestApi.Database.Data.Entities
{
    public class Stock : BaseEntity
    {
        public Stock()
        {
            History = new HashSet<StockHistory>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<StockHistory> History { get; set; }
    }
}
