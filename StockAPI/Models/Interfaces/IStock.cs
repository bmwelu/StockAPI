using System.Collections.Generic;

namespace StockAPI.Models.Interfaces
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