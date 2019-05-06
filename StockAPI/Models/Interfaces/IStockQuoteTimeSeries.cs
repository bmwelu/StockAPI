using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace StockAPI.Models.Interfaces
{
    public interface IStockQuoteTimeSeries
    {
        IList<ITimeSeriesData> TimeSeriesData { get; set; }
        IEnumerable<ITimeSeriesData> ParseTimeSeriesData(JArray timeSeriesData);
    }
}