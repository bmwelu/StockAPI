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
    public class StockController : ControllerBase
    {
        private IStockService _stockService;
        private readonly ILogger _logger;
        public StockController(IStockService stockService, ILogger<AuthController> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }
        [HttpGet("{ticker}/detail"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IStock>> GetStockDetail(string ticker)
        {
            try
            {
                return Ok(await _stockService.GetStockDetail(ticker));
            }
            catch(Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in StockController: GetStockDetail\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
        [HttpGet("{ticker}/news"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<INews>> GetStockNews(string ticker)
        {
            try
            {
                return Ok(await _stockService.GetStockNews(ticker));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in StockController: GetStockNews\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
        [HttpGet("{ticker}/previousclose"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IStockPreviousClose>> GetStockPreviousClose(string ticker)
        {
            try
            {
                return Ok(await _stockService.GetStockPreviousClose(ticker));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in StockController: GetStockPreviousClose\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
        [HttpGet("{ticker}/timeseries/{interval}"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IEnumerable<ITimeSeriesData>>> GetStockTimeSeriesData(string ticker, int interval)
        {
            try
            {
                return Ok(await _stockService.GetTimeSeriesData(ticker, interval));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "Exception in StockController: GetStockTimeSeriesData\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}