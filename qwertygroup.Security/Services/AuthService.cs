using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthUserService.IAuthUserService _authUserService;
        private readonly IAuthUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IConfiguration configuration,
            IAuthUserService.IAuthUserService authUserService,
            IAuthUserRepository _userRepository)
        {
            _authUserService = authUserService;
            this._userRepository = _userRepository;
            _configuration = configuration;
        }

        


        public JwtToken GenerateJwtToken(string username, string password)
        {
            var dbUser = _userRepository.FindUser(username);
            
            if (!Authenticate(dbUser, password))
                return new JwtToken
                {
                    Message = "User or Password not correct"
                };
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                new[]
                {
                    new Claim("Id", dbUser.Id.ToString())
                },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);
            
            return new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Ok"
            };

        }

        private bool Authenticate(AuthUser dbUser, string plainTextPassword)
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

        public AuthUser FindUser(string username)
        {
            return _userRepository.FindUser(username);
        }

        public bool CreateUser(AuthUser user, string registerDtoPassword)
        {
            var salt = CreateSalt();
            var newUser = new AuthUser()
            {
                Username = user.Username,
                Email = user.Email,
                Salt = salt,
                HashedPassword = HashedPassword(registerDtoPassword, salt)
            };
            return _userRepository.CreateUser(newUser);
        }

        public AuthUser CreateUser(IdentityUser identityUser, string registerDtoPassword)
        {
            throw new NotImplementedException();
        }

        public List<Permission> GetPermissions(int id)
        {
            return _userRepository.GetUserPermissions(id);
        }

        public bool DeleteUser(AuthUser user)
        {
            return _userRepository.DeleteUser(user);
        }

        public ActionResult<List<AuthUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}