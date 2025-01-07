namespace StockLogger.Models.DTO
{
  public class GetStockPriceRequestDto
  {
    public string Ticker { get; set; }
    public string Exchange { get; set; }
  }
}
