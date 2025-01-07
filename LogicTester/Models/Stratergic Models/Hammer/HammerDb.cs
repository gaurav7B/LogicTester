using StockLogger.Models.Stratergic_Models.Inverted_Hammer;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Hammer
{
    public class HammerDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsHammerDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<HammerCandels>? HammerCandels { get; set; }
    }
}
