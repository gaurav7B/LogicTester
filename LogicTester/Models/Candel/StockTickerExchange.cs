using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Candel
{
    public class StockTickerExchange
    {
        [Key]
        public long Id { get; set; }

        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public string CompanyName { get; set; }
    }
}
