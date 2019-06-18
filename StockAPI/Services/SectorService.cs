using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models.Interfaces;
using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StockAPI.Services.Interfaces;

namespace StockAPI.Services
{
    public class SectorService : ISectorService
    {
        private readonly IConfiguration _iConfig;
        private readonly IMapper _mapper;
        private readonly string _realTimeSectionString = "Rank A: Real-Time Performance";
        public SectorService(IConfiguration iConfig, IMapper mapper)
        {
            _iConfig = iConfig;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ISector>> GetSectors()
        {
            try
            {           
                var sectorsJSONResponse = JObject.Parse(await GetExternalResponse())[_realTimeSectionString];
                if (sectorsJSONResponse ==  null)
                    throw new NullReferenceException("Real time sector data is null.");
                var realTimeSectorData = new List<JProperty>();
                foreach (var jToken in sectorsJSONResponse.Children())
                {
                    realTimeSectorData.Add((JProperty)jToken);
                }
                return _mapper.Map<IEnumerable<Sector>>(realTimeSectorData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetExternalResponse()
        {
            var response = await new HttpClient().GetAsync(_iConfig.GetValue<string>("ExternalApiURLs:SectorURL"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
