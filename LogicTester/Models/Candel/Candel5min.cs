using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Candel
{
    public class Candel5min
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

        // Volume data
        public decimal Volume { get; set; }
        //public decimal Turnover { get; set; }  // Optional, for total trade value

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

        //// Calculated properties (moving average and Bollinger bands)
        //public decimal? MovingAverage { get; private set; }
        //public decimal? BollingerUpper { get; private set; }
        //public decimal? BollingerLower { get; private set; }

        //// Method to calculate Moving Average
        //public void CalculateMovingAverage(List<decimal> closingPrices)
        //{
        //    if (closingPrices == null || closingPrices.Count == 0)
        //        throw new ArgumentException("Closing prices list cannot be empty.");

        //    MovingAverage = closingPrices.Sum() / closingPrices.Count;
        //}

        //// Method to calculate Bollinger Bands
        //public void CalculateBollingerBands(List<decimal> closingPrices)
        //{
        //    if (closingPrices == null || closingPrices.Count < 2)
        //        throw new ArgumentException("At least 2 closing prices are required for Bollinger Bands calculation.");

        //    CalculateMovingAverage(closingPrices);  // Ensure MovingAverage is calculated

        //    // Calculate the standard deviation
        //    decimal sumOfSquares = closingPrices.Sum(price => (price - MovingAverage.Value) * (price - MovingAverage.Value));
        //    decimal standardDeviation = (decimal)Math.Sqrt((double)(sumOfSquares / closingPrices.Count));

        //    // Calculate upper and lower bands
        //    BollingerUpper = MovingAverage + (2 * standardDeviation);
        //    BollingerLower = MovingAverage - (2 * standardDeviation);
        //}

    }
}
