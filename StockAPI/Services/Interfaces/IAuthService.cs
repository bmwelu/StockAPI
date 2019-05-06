using StockAPI.Models.Interfaces;

namespace StockAPI.Services.Interfaces
{
    public interface IAuthService
    {
        string GetAuthToken(ILogin user);
    }
}