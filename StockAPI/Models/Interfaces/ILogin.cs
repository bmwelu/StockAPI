namespace StockAPI.Models.Interfaces
{
    public interface ILogin
    {
        string Password { get; set; }
        string UserName { get; set; }
    }
}