namespace StockAPI.Models
{
    public interface IStockPreviousClose
    {
        string PreviousClose { get; set; }
        string Symbol { get; set; }
    }
}