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
    public class MarketController : ControllerBase
    {
        private IMarketService _marketService;
        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }

        [HttpGet("news"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<INews>>> GetMarketNews()
        {
            try
            {
                return Ok(await _marketService.GetMarketNews());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}