namespace StockAPI.Models
{
    public interface ILogin
    {
        string Password { get; set; }
        string UserName { get; set; }
    }
}