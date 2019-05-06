namespace StockAPI.Models.Interfaces
{
    public interface IStockPreviousClose
    {
        string PreviousClose { get; set; }
        string Symbol { get; set; }
    }
}