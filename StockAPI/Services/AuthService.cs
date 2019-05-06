using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StockAPI.Models.Interfaces;
using StockAPI.Services.Interfaces;

namespace StockAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _iConfig;

        public AuthService(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }
        public string GetAuthToken(ILogin credentials)
        {
            try
            {
                if (credentials.UserName == "brandon" && credentials.Password == "welu")
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: _iConfig.GetValue<string>("Hosting:APIURL"),
                        audience: _iConfig.GetValue<string>("Hosting:APIURL"),
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(int.Parse(_iConfig.GetValue<string>("Hosting:TokenExpirationMinutes"))),
                        signingCredentials: signinCredentials
                    );

                    return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
