using System.Collections.Generic;
using NewsAPI.Models;
using StockAPI.Models.Interfaces;

namespace StockAPI.Models
{
    public class Stock : IStock
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string PrimaryExchange { get; set; }
        public float Week52High { get; set; }
        public float Week52Low { get; set; }
        public float LatestPrice { get; set; }
        public float PreviousClose { get; set; }
        public IEnumerable<Article> StockNews { get; set; } = new List<Article>();
        public string Sector { get; set; }
    }
}
