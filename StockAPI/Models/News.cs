using StockAPI.Models.Interfaces;

namespace StockAPI.Models
{
    public class News : INews
    {
        public string Headline { get; set; }
        public string URL { get; set; }
        public string Source { get; set; }
    }
}
