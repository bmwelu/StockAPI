using System.Collections.Generic;

namespace StockAPI.Models
{
    public static class Constaints
    {
        public static readonly Dictionary<int, string> TimeSeriesAPIMapping = new Dictionary<int, string>()
        {
            {0, "dynamic" },
            {1, "1m" },
            {2, "1y" },
            {3, "5y" },
        };
    }
}
