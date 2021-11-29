using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Encodings;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IAuthUserService.IAuthUserService _authUserService;
        private readonly UserManager<IdentityUser> _userManager;

        public SecurityService(
            IConfiguration configuration,
            IAuthUserService.IAuthUserService authUserService,
            UserManager<IdentityUser> userManager)
        {
            _authUserService = authUserService;
            _userManager = userManager;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public JwtToken GenerateJwtToken(string username, string password)
        {
            if (!Authenticate(password, username))
                return new JwtToken
                {
                    Message = "User or Password not correct"
                };
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(Configuration["JwtConfig:Issuer"],
                Configuration["JwtConfig:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);
            
            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Ok"
            };

        }

        private bool Authenticate(string plainTextPassword, string username)
        {
            var identityUser = _userManager.FindByNameAsync(username).Result;
            if (identityUser == null) return false;
            
            var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, plainTextPassword);
            return result != PasswordVerificationResult.Failed;

        }

        public string HashedPassword(string plainTextPassword, byte[] userSalt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainTextPassword,
                salt: userSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}