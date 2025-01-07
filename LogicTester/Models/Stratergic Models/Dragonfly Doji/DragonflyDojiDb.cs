using StockLogger.Models.Stratergic_Models.Bullish_Harami;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Dragonfly_Doji
{
    public class DragonflyDojiDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsDragonflyDojiDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<DragonflyDojiCandels>? DragonflyDojiCandels { get; set; }
    }
}
