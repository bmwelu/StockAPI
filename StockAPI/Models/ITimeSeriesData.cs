namespace StockAPI.Models
{
    public interface ITimeSeriesData
    {
        string Time { get; set; }
        float? Value { get; set; }
    }
}