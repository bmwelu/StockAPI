using System.Collections.Generic;

namespace StockAPI.Models
{
    public interface IStock
    {
        string CompanyName { get; set; }
        float LatestPrice { get; set; }
        float PreviousClose { get; set; }
        string PrimaryExchange { get; set; }
        IEnumerable<INews> StockNews { get; set; }
        string Symbol { get; set; }
        float Week52High { get; set; }
        float Week52Low { get; set; }
        string Sector { get; set; }
    }
}