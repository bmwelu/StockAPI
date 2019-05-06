using StockAPI.Models.Interfaces;
namespace StockAPI.Models
{
    public class SuggestedStock : ISuggestedStock
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
    }
}
