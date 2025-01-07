using System.ComponentModel.DataAnnotations;

namespace StockLogger.Models.Candel
{
    public class Token
    {
        [Key]
        public long Id { get; set; }
        public string AuthToken { get; set; }
    }
}
