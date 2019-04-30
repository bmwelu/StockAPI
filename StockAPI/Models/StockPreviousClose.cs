namespace StockAPI.Models
{
    public class StockPreviousClose : IStockPreviousClose
    {
        public string Symbol { get; set; }
        public string PreviousClose { get; set; }
    }
}
