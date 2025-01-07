using StockLogger.Models.Stratergic_Models.Rising_Three_Methods;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models.Tower_Bottom
{
    public class TowerBottomDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsTowerBottomDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<TowerBottomCandels>? Candels { get; set; }
    }
}
