using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models;
using StockAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using StockAPI.Services.Interfaces;

namespace StockAPI.Services
{
    public class StockService : IStockService
    {
        private readonly IConfiguration _iConfig;
        private readonly IMapper _mapper;
        public StockService(IConfiguration iConfig, IMapper mapper)
        {
            _iConfig = iConfig;
            _mapper = mapper;
        }
        public async Task<IStock> GetStockDetail(string ticker)
        {
            try
            {
                var stockDetail = JsonConvert.DeserializeObject<Stock>(await GetExternalStockDetailResponse(ticker));
                stockDetail.StockNews = await GetStockNews(ticker);
                return stockDetail;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<INews>> GetStockNews(string ticker)
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<News>>(await GetExternalStockNewsResponse(ticker));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<IStockQuote>> GetStockQuotes(string[] tickers)
        {
            try
            {
                var quotesJson = new List<JProperty>();
                foreach (var jToken in JObject.Parse(await GetExternalStockQuotesResponse(tickers)).Children())
                {
                    quotesJson.Add((JProperty)jToken);
                }
                return _mapper.Map<IEnumerable<StockQuote>>(quotesJson);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<ITimeSeriesData>> GetTimeSeriesData(string ticker, int interval)
        {
            if(interval < int.Parse(_iConfig.GetValue<string>("TimeSeriesInvervals:Intraday")) || 
                interval > int.Parse(_iConfig.GetValue<string>("TimeSeriesInvervals:Monthly")))
            {
                throw new IndexOutOfRangeException($"Interval passed in is out of range.  Valid range is " +
                    $"{_iConfig.GetValue<string>("TimeSeriesInvervals:Intraday")} to " +
                    $"{int.Parse(_iConfig.GetValue<string>("TimeSeriesInvervals:Weekly"))}");
            }
            try
            {
                var apiIntervalString = Constaints.TimeSeriesAPIMapping[interval];
                var timeSeriesJson = JArray.Parse(await GetExternalStockTimeSeriesResponse(ticker, apiIntervalString));
                if (interval == int.Parse(_iConfig.GetValue<string>("TimeSeriesInvervals:Intraday")))
                {
                    return new StockQuoteOneDayTimeSeries(_iConfig).ParseTimeSeriesData(timeSeriesJson);
                }
                else
                {
                    return new StockQuoteTimeSeries().ParseTimeSeriesData(timeSeriesJson);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IStockPreviousClose> GetStockPreviousClose(string ticker)
        {
            try
            {
                //expect only a list of 1
                var quoteList = new List<JToken>(JObject.Parse(await GetExternalStockPreviousCloseResponse(ticker)).Children());
                return _mapper.Map<StockPreviousClose>((JProperty)quoteList[0]);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<ISuggestedStock>> GetSuggestedStocks(string searchString)
        {
            try
            {
                var suggestedStockJson = new List<JObject>();
                foreach (var jToken in JObject.Parse(await GetExternalStockLookupResponse(searchString))["ResultSet"]["Result"].Children())
                {
                    suggestedStockJson.Add((JObject)jToken);
                }
                return _mapper.Map<List<SuggestedStock>>(suggestedStockJson).Take(
                    int.Parse(_iConfig.GetValue<string>("SuggestedStocks:MaxResults")));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<string> GetExternalStockDetailResponse(string ticker)
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockURL")}/" +
                $"{ticker}/quote");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> GetExternalStockNewsResponse(string ticker)
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockURL")}/" +
                $"{ticker}/news/last/" +
                $"{_iConfig.GetValue<string>("Stock:MaxNewsArcticles")}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> GetExternalStockQuotesResponse(string[] tickers)
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockURL")}market/batch?symbols=" +
                $"{string.Join(',', tickers)}&types=quote&range=1m");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> GetExternalStockPreviousCloseResponse(string ticker)
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockURL")}market/batch?symbols=" +
                $"{ticker}&types=quote&range=1m");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> GetExternalStockLookupResponse(string searchString)
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockLookup")}" +
                $"{searchString}&region=US&lang=en-US");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> GetExternalStockTimeSeriesResponse(string ticker, string timeSpan)
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockURL")}" +
                $"{ticker}/chart/{timeSpan}/?changeFromClose=true");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
