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
    public class SectorsController : ControllerBase
    {
        private ISectorService _sectorService;
        private readonly ILogger _logger;
        public SectorsController(ISectorService sectorService, ILogger<AuthController> logger)
        {
            _sectorService = sectorService;
            _logger = logger;
        }

        [HttpGet, Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IEnumerable<ISector>>> Get()
        {
            try
            {
                return Ok(await _sectorService.GetSectors());
            }
            catch(Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in SectorsController: Get\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}