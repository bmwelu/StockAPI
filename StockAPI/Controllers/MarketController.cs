using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAPI.Models;
using StockAPI.Models;
using StockAPI.Models.Interfaces;
using StockAPI.Services.Interfaces;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private IMarketService _marketService;
        private readonly ILogger _logger;
        public MarketController(IMarketService marketService, ILogger<AuthController> logger)
        {
            _marketService = marketService;
            _logger = logger;
        }

        [HttpGet("news"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IEnumerable<Article>>> GetMarketNews()
        {
            try
            {
                return Ok(await _marketService.GetMarketNews());
            }
            catch(Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in MarketController: GetMarketNews\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}