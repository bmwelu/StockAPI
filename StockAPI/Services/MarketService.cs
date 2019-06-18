using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StockAPI.Services.Interfaces;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;

namespace StockAPI.Services
{
    public class MarketService : IMarketService
    {
        private readonly IConfiguration _iConfig;

        public MarketService(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }
        public async Task<IEnumerable<Article>> GetMarketNews()
        {
            try
            {
                return await GetTopNewsAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IList<Article>> GetTopNewsAsync()
        {
            // init with your API key
            var newsApiClient = new NewsApiClient(_iConfig.GetValue<string>("ExternalApiURLs:NewsAPIKey"));
            var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Country = Countries.US,
                PageSize = 10,
                Page = 1
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                return articlesResponse.Articles;
            }
            return null;
        }
    }
}
