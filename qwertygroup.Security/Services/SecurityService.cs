using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthUserService _authUserService;

        public SecurityService(IConfiguration configuration,
            IAuthUserService authUserService)
        {
            _configuration = configuration;
            _authUserService = authUserService;
        }
        public JwtToken GenerateJwtToken(string username, string password)
        {
            var user = _authUserService.Login(username, password);
            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"], 
                    _configuration["JwtConfig:Audience"],
                    null,
                    expires:DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "ok"
                };
            }
            else
            {
                return new JwtToken()
                {
                    Message = "Username or password not found"
                };
            }
            
        }
    }
}