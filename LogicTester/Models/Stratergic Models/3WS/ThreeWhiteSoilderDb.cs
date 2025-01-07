using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Stratergic_Models
{
    public class ThreeWhiteSoilderDb
    {
        [Key]
        public long Id { get; set; }  // Primary key

        public string Ticker { get; set; }
        public long TickerId { get; set; }
        public string Exchange { get; set; }

        public bool IsThreeWhiteSoilderDetected { get; set; }

        public int DetectionRange { get; set; }
        public DateTime DetectionTime { get; set; }

        // Navigation property to hold the list of Candel objects
        public List<ThreeWhiteSoilderCandels>? ThreeWhiteSoilderCandels { get; set; }

    }
}
