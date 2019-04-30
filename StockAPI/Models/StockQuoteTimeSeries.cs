using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace StockAPI.Models
{
    public class StockQuoteTimeSeries : IStockQuoteTimeSeries
    {
        public IList<ITimeSeriesData> TimeSeriesData { get; set; } = new List<ITimeSeriesData>();
        public virtual IEnumerable<ITimeSeriesData> ParseTimeSeriesData(JArray timeSeriesData)
        {
            foreach (var jToken in timeSeriesData.Children())
            {
                var interval = (JObject)jToken;
                TimeSeriesData.Add(new TimeSeriesData() { Time = jToken["date"].ToString(), Value = float.Parse(jToken["close"].ToString()) });
            }
            return TimeSeriesData;
        }
    }
}
