using StockLogger.Models.Stratergic_Models.Marubozu__Bullish_;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Rising_Sun
{
    public class RisingSunDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsRisingSunDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<RisingSunCandels>? Candels { get; set; }
    }
}
