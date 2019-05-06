namespace StockAPI.Models.Interfaces
{
    public interface ISuggestedStock
    {
        string CompanyName { get; set; }
        string Symbol { get; set; }
    }
}