using System.Collections.Generic;
using System.Threading.Tasks;
using StockAPI.Models.Interfaces;

namespace StockAPI.Services.Interfaces
{
    public interface IMarketService
    {
        Task<IEnumerable<INews>> GetMarketNews();
    }
}