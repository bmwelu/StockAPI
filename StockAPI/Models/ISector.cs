namespace StockAPI.Models
{
    public interface ISector
    {
        string PercentChange { get; set; }
        string SectorName { get; set; }
    }
}