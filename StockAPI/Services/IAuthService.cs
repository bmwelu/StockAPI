using System.Threading.Tasks;
using StockAPI.Models;

namespace StockAPI.Services
{
    public interface IAuthService
    {
        string GetAuthToken(ILogin user);
    }
}