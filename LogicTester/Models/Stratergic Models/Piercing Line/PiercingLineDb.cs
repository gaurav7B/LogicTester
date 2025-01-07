using StockLogger.Models.Stratergic_Models.Morning_Star;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Piercing_Line
{
    public class PiercingLineDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsPiercingLineDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<PiercingLineCandels>? PiercingLineCandels { get; set; }
    }
}
