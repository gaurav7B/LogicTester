using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Bullish_Harami
{
    public class BullishHaramiDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsBullishHaramiDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<BullishHaramiCandels>? BullishHaramiCandels { get; set; }
    }
}
