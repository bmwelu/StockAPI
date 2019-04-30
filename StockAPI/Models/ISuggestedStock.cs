namespace StockAPI.Models
{
    public interface ISuggestedStock
    {
        string CompanyName { get; set; }
        string Symbol { get; set; }
    }
}