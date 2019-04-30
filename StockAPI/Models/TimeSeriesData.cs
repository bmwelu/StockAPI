namespace StockAPI.Models
{
    public class TimeSeriesData : ITimeSeriesData
    {
        public string Time { get; set; }
        public float? Value { get; set; }
    }
}
