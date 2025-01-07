using StockLogger.Models.Stratergic_Models.Inverted_Hammer;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Morning_Star
{
    public class MorningStarDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsMorningStarDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<MorningStarCandels>? MorningStarCandels { get; set; }
    }
}
