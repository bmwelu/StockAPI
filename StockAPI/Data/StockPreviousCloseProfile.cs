using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class StockPreviousCloseProfile : Profile
    {
        public StockPreviousCloseProfile()
        {
            CreateMap<JProperty, StockPreviousClose>()
                .ForMember(s => s.Symbol, opt => opt.MapFrom(jprop => jprop.Name))
                .ForMember(s => s.PreviousClose, opt => opt.MapFrom(jprop => jprop.Value["quote"]["previousClose"]));
        }
    }
}
