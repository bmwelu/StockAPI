using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAPI.Models;
using StockAPI.Models.Interfaces;

namespace StockAPI.Services.Interfaces
{
    public interface IMarketService
    {
        Task<IEnumerable<Article>> GetMarketNews();
    }
}