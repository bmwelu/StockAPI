namespace StockAPI.Models.Interfaces
{
    public interface ISector
    {
        string PercentChange { get; set; }
        string SectorName { get; set; }
    }
}