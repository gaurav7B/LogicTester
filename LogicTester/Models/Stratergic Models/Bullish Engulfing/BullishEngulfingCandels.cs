using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Bullish_Engulfing
{
    public class BullishEngulfingCandels
    {
        [Key]
        public long Id { get; set; }  // Primary key

        // Core properties
        public decimal StartPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal EndPrice { get; set; }

        // Time information
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }

        // Meta-information
        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        // BULL BEAR properties
        public bool? IsBullish { get; set; }
        public bool? IsBearish { get; set; }

        // Method to set bullish or bearish status based on prices
        public void SetBullBearStatus()
        {
            if (EndPrice > StartPrice)
            {
                IsBullish = true; IsBearish = null;
            }
            else if (EndPrice < StartPrice)
            {
                IsBullish = null; IsBearish = true;
            }
            else
            {
                IsBullish = null; IsBearish = null;
            }
        }


        // Calculated Properties
        public decimal PriceChange { get; set; }
        public decimal PriceChangePercentage { get; set; }

        // Method to set bullish or bearish status based on prices
        public void SetPriceChange()
        {
            PriceChange = EndPrice - StartPrice;
            PriceChangePercentage = StartPrice != 0 ? (PriceChange / StartPrice) * 100 : 0;
        }

        // Volume data
        public decimal Volume { get; set; }

    }
}
