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
    public class StockController : ControllerBase
    {
        private IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet("{ticker}/detail"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IStock>> GetStockDetail(string ticker)
        {
            try
            {
                return Ok(await _stockService.GetStockDetail(ticker));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
        [HttpGet("{ticker}/news"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<INews>> GetStockNews(string ticker)
        {
            try
            {
                return Ok(await _stockService.GetStockNews(ticker));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
        [HttpGet("{ticker}/previousclose"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IStockPreviousClose>> GetStockPreviousClose(string ticker)
        {
            try
            {
                return Ok(await _stockService.GetStockPreviousClose(ticker));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
        [HttpGet("{ticker}/timeseries/{interval}"), Authorize]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<IEnumerable<ITimeSeriesData>>> GetStockTimeSeriesData(string ticker, int interval)
        {
            try
            {
                return Ok(await _stockService.GetTimeSeriesData(ticker, interval));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}