using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using StockAPI.Models.Interfaces;

namespace StockAPI.Models
{
    public class StockQuoteOneDayTimeSeries : IStockQuoteTimeSeries
    {
        private TimeSpan TradingStartTime = new TimeSpan(8, 30, 0);
        private readonly int MinutesInOneDayTradingSession = 390;
        private readonly IConfiguration _iConfig;
        public IList<ITimeSeriesData> TimeSeriesData { get; set; } = new List<ITimeSeriesData>();
        public StockQuoteOneDayTimeSeries(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }
        public virtual IEnumerable<ITimeSeriesData> ParseTimeSeriesData(JArray timeSeriesData)
        {
            if(timeSeriesData != null && timeSeriesData.Count > 0)
            {
                //Sometimes stocks don't trade right away, so averages will be -1.  Find the first entry that has an average
                var firstTradingTimeDurationIndex = FindFirstTradingTimeDuration(timeSeriesData);
                //There hasn't been anything traded so far today, return empty list
                if (firstTradingTimeDurationIndex == -1)
                {
                    return TimeSeriesData;
                }
                //Add previous close
                //Current data doesn't set the first point the previous close.  If there is aftermarket trading, graphs won't
                //display % change correctly.
                var previousCloseInterval = CalculatePreviousClose((JObject)timeSeriesData[firstTradingTimeDurationIndex]);
                TimeSeriesData.Add(previousCloseInterval);
                var previousAverage = previousCloseInterval.Value;
                for (int i = 1; i <= MinutesInOneDayTradingSession + 1; i++)
                {
                    if (i <= timeSeriesData.Count)
                    {
                        var timeInterval = (JObject)timeSeriesData[i - 1];
                        //sometimes stocks aren't traded at all during a minute duration, get previous average if so
                        if (float.Parse(timeInterval["average"].ToString()) == -1)
                        {
                            TimeSeriesData.Add(new TimeSeriesData() { Time= TradingStartTime.ToString(), Value = previousAverage });
                        }
                        else
                        {
                            TimeSeriesData.Add(new TimeSeriesData() { Time = TradingStartTime.ToString(), Value = float.Parse(timeInterval["average"].ToString()) });
                            previousAverage = float.Parse(timeInterval["average"].ToString());
                        }
                    }
                    else
                    {
                        TimeSeriesData.Add(new TimeSeriesData() { Time = TradingStartTime.ToString(), Value = null });
                    }
                    TradingStartTime = TradingStartTime.Add(new TimeSpan(0, 1, 0));
                }               
            }
            return new List<ITimeSeriesData>(TimeSeriesData.Where((x, i) => i % 
                int.Parse(_iConfig.GetValue<string>("OneDayTimeSeries:MinutesToSkip")) == 0));
        }       
        private int FindFirstTradingTimeDuration(JArray timeSeriesData)
        {
            for (int i = 0; i < timeSeriesData.Count; i++)
            {
                if (float.Parse(((JObject)timeSeriesData[i])["average"].ToString()) != -1)
                {
                    return i;
                }
            }
            return -1;
        }
        private ITimeSeriesData CalculatePreviousClose(JObject firstInterval)
        {
            return new TimeSeriesData() { Time = "previousClose", Value = float.Parse(firstInterval["average"].ToString())
                * (1 - float.Parse(firstInterval["changeOverTime"].ToString()))};
        }
    }
}
