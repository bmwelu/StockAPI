namespace StockAPI.Models.Interfaces
{
    public interface ITimeSeriesData
    {
        string Time { get; set; }
        float? Value { get; set; }
    }
}