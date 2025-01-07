using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models
{
    public class Report
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string ReportType { get; set; }
        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        public decimal ReportCandelClosePrice { get; set; }
        public decimal NextCandelClosePrice { get; set; }

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool? IsPredictionTrue { get; set; }
        public bool? IsPredictionFalse { get; set; }

        public void SetNextCandelBullBearStatus()
        {
            if (NextCandelClosePrice > ReportCandelClosePrice)
            {
                IsPredictionTrue = true; IsPredictionFalse = null;
            }
            else if (NextCandelClosePrice < ReportCandelClosePrice)
            {
                IsPredictionTrue = null; IsPredictionFalse = true;
            }
            else
            {
                IsPredictionTrue = null; IsPredictionFalse = null;
            }
        }

        public decimal PriceChange { get; set; }
        public decimal PriceChangePercentage { get; set; }

        // Method to set bullish or bearish status based on prices
        public void SetPriceChange()
        {
            PriceChange = NextCandelClosePrice - ReportCandelClosePrice;
            PriceChangePercentage = ReportCandelClosePrice != 0 ? (PriceChange / ReportCandelClosePrice) * 100 : 0;
        }
    }
}
