using StockAPI.Models.Interfaces;

namespace StockAPI.Models
{
    public class Sector : ISector
    {
        public string SectorName { get; set; }
        public string PercentChange { get; set; }
    }
}
