using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class StockQuoteProfile : Profile
    {
        public StockQuoteProfile()
        {
            CreateMap<JProperty, StockQuote>()
                .ForMember(s => s.Symbol, opt => opt.MapFrom(jprop => jprop.Name))
                .ForMember(s => s.LatestPrice, opt => opt.MapFrom(jprop => jprop.Value["quote"]["latestPrice"]))
                .ForMember(s => s.PercentChange, opt => opt.MapFrom(
                    jprop => $"{(float.Parse(jprop.Value["quote"]["changePercent"].ToString()) * 100).ToString("0.00")}%"));
        }
    }
}
