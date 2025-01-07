using StockLogger.Models.Stratergic_Models.Bullish_Abandoned_Baby;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Bullish_Belt_Hold
{
    public class BeltHoldDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsBeltHoldDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<BeltHoldCandels>? Candels { get; set; }
    }
}
