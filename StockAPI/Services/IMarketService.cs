using System.Collections.Generic;
using System.Threading.Tasks;
using StockAPI.Models;

namespace StockAPI.Services
{
    public interface IMarketService
    {
        Task<IEnumerable<INews>> GetMarketNews();
    }
}