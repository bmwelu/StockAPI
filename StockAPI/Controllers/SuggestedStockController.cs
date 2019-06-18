
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
    public class SuggestedStockController : ControllerBase
    {
        private IStockService _stockService;
        private readonly ILogger _logger;
        public SuggestedStockController(IStockService stockService, ILogger<AuthController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        [HttpGet(), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IEnumerable<ISuggestedStock>>> GetSuggestedStocks([FromQuery(Name = "searchString")] string searchString)
        {
            try
            {
                if (string.IsNullOrEmpty(searchString))
                    return BadRequest("Search string can't be empty.");
                return Ok(await _stockService.GetSuggestedStocks(searchString));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in SuggestedStockController: GetSuggestedStocks\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}