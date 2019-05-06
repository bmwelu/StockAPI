using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockAPI.Models;
using StockAPI.Models.Interfaces;
using StockAPI.Services.Interfaces;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class StockQuoteController : ControllerBase
    {
        private IStockService _stockService;
        private readonly ILogger _logger;
        public StockQuoteController(IStockService stockService, ILogger<AuthController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet(), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IEnumerable<IStockQuote>>> GetStockQuotes([FromQuery(Name = "tickers")] string[] tickers)
        {
            try
            {
                return Ok(await _stockService.GetStockQuotes(tickers));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in StockQuoteController: GetStockQuotes\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}