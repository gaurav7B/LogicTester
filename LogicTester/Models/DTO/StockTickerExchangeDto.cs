namespace StockLogger.Models.DTO
{
    public class StockTickerExchangeDto
    {
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public string? CompanyName { get; set; }
    }
}
