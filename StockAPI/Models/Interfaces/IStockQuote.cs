namespace StockAPI.Models.Interfaces
{
    public interface IStockQuote
    {
        float? LatestPrice { get; set; }
        string PercentChange { get; set; }
        string Symbol { get; set; }
    }
}