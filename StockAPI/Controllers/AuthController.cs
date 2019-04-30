using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockAPI.Models;
using StockAPI.Services;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost, Route("login")]
        [MapToApiVersion("1.0")]
        [EnableCors("AllowOrigin")]
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
                    return Ok(new { Token = authToken });
                else
                    return Unauthorized();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server.");
            }
        }
    }
}