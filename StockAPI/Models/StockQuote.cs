namespace StockAPI.Models
{
    public class StockQuote : IStockQuote
    {
        public string Symbol { get; set; }
        public float? LatestPrice { get; set; }
        public string PercentChange { get; set; }
    }
}
