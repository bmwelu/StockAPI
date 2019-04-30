
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockAPI.Models;
using StockAPI.Services;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SuggestedStockController : ControllerBase
    {
        private IStockService _stockService;
        public SuggestedStockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet(), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<ISuggestedStock>>> GetSuggestedStocks([FromQuery(Name = "searchString")] string searchString)
        {
            try
            {
                return Ok(await _stockService.GetSuggestedStocks(searchString));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}