using AutoMapper;
using Newtonsoft.Json.Linq;
using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
                var sectorsJson = new List<JProperty>();
                foreach(var jToken in JObject.Parse(await GetExternalResponse())[_realTimeSectionString].Children())
                {
                    sectorsJson.Add((JProperty)jToken);
                }
                return _mapper.Map<IEnumerable<Sector>>(sectorsJson);
            }
            catch (Exception e)
            {
                throw e;
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
