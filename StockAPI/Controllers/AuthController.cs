using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockAPI.Models;
using StockAPI.Services.Interfaces;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private readonly ILogger _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost, Route("login")]
        [MapToApiVersion("1.0")]
        [EnableCors("CorsPolicy")]
        public IActionResult Login([FromForm]Login credentials)
        {
            try
            {
                if (credentials == null)
                {
                    return BadRequest("Invalid client request");
                }
                var authToken = _authService.GetAuthToken(credentials);
                if (!string.IsNullOrEmpty(authToken))
                {
                    _logger.LogInformation(LoggingEvents.Login, $"{credentials.UserName} successfully logged in.");
                    return Ok(new { Token = authToken });
                }                   
                else
                    return Unauthorized();
            }
            catch(Exception ex)
            {
                _logger.LogWarning(LoggingEvents.LoginNotFound, ex, "Exception in AuthController: Login\n");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}