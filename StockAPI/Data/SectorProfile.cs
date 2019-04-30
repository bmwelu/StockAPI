using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class SectorProfile : Profile
    {
        public SectorProfile()
        {
            CreateMap<JProperty, Sector>()
                .ForMember(s => s.SectorName, opt => opt.MapFrom(jprop => jprop.Name))
                .ForMember(s => s.PercentChange, opt => opt.MapFrom(jprop => jprop.Value));
        }
    }
}
