using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtToken GenerateJwtToken(AuthUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email)
                },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Ok"
            };
        }
        
        public bool Authenticate(AuthUser dbUser, string plainTextPassword)
        {
            return dbUser != null && HashedPassword(plainTextPassword, dbUser.Salt)
                .Equals(dbUser.HashedPassword);
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

        public byte[] CreateSalt()
        {
            // 128-bit salt
            var rng = new RNGCryptoServiceProvider();
            var salt = new byte[128 / 8];
            rng.GetNonZeroBytes(salt);

            return salt;
        }
    }
}