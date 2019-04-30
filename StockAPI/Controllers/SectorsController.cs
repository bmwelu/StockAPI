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
    public class SectorsController : ControllerBase
    {
        private ISectorService _sectorService;
        public SectorsController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet, Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<ISector>>> Get()
        {
            try
            {
                return Ok(await _sectorService.GetSectors());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}