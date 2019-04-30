using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class SuggestedStockProfile : Profile
    {
        public SuggestedStockProfile()
        {
            CreateMap<JObject, SuggestedStock>()
                .ForMember(s => s.Symbol, opt => opt.MapFrom(jobj => jobj.GetValue("symbol")))
                .ForMember(s => s.CompanyName, opt => opt.MapFrom(jobj => jobj.GetValue("name")));
        }
    }
}
