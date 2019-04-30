using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockAPI.Models;

namespace StockAPI.Services
{
    public interface IStockService
    {
        Task<IStock> GetStockDetail(string ticker);
        Task<IEnumerable<INews>> GetStockNews(string ticker);
        Task<IEnumerable<IStockQuote>> GetStockQuotes(string[] tickers);
        Task<StockPreviousClose> GetStockPreviousClose(string ticker);
        Task<IEnumerable<ISuggestedStock>> GetSuggestedStocks(string searchString);
        Task<IEnumerable<ITimeSeriesData>> GetTimeSeriesData(string ticker, int interval);
    }
}