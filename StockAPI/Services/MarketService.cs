using StockAPI.Models.Interfaces;
using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StockAPI.Services.Interfaces;

namespace StockAPI.Services
{
    public class MarketService : IMarketService
    {
        private readonly IConfiguration _iConfig;

        public MarketService(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }
        public async Task<IEnumerable<INews>> GetMarketNews()
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<News>>(await GetExternalStockNewsResponse());
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<string> GetExternalStockNewsResponse()
        {
            var response = await new HttpClient().GetAsync(
                $"{_iConfig.GetValue<string>("ExternalApiURLs:StockURL")}/" +
                $"market/news/last/" +
                $"{_iConfig.GetValue<string>("Market:MaxNewsResults")}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
