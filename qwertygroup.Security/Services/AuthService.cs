using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IAuthUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IConfiguration configuration,
            IAuthUserRepository userRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }




        public JwtToken GenerateJwtToken(AuthUser user, string password)
        {
            if (!Authenticate(user, password))
                return new JwtToken
                {
                    Message = "Email or Password not correct"
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                new[]
                {
                    new Claim("Id", user.Id.ToString())
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

        //TODO refac
        public AuthUser FindUser(string username)
        {
            return _userRepository.FindUser(username);
        }

        //TODO refac
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

        public List<Permission> GetPermissions(int id)
        {
            return _userRepository.GetUserPermissions(id);
        }

        //TODO refac
        public bool DeleteUser(AuthUser user)
        {
            return _userRepository.DeleteUser(user);
        }

        //TODO refac
        public bool AdminDeleteUser(AuthUser user)
        {
            return _userRepository.DeleteUser(user);
        }

        //TODO refac
        public List<AuthUser> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public bool AssignAdminPermissionToUser(AuthUser user)
        {
            return _userRepository.AssignAdminPermissionToUser(user);
        }

        public bool RemoveAdminPermissionFromUser(AuthUser user)
        {
            return _userRepository.RemoveAdminPermissionFromUser(user);
        }
    }
}