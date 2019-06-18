using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAPI.Models;
using StockAPI.Models.Interfaces;

namespace StockAPI.Services.Interfaces
{
    public interface IStockService
    {
        Task<IStock> GetStockDetail(string ticker);
        Task<IEnumerable<Article>> GetStockNews(string ticker);
        Task<IEnumerable<IStockQuote>> GetStockQuotes(string[] tickers);
        Task<IStockPreviousClose> GetStockPreviousClose(string ticker);
        Task<IEnumerable<ISuggestedStock>> GetSuggestedStocks(string searchString);
        Task<IEnumerable<ITimeSeriesData>> GetTimeSeriesData(string ticker, int interval);
    }
}