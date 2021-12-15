using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using qwertygroup.Security.Helpers;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(
            IConfiguration configuration,
            IAuthUserRepository userRepository)
        {
            if (configuration == null)
            {
                throw new InvalidDataException("AuthService must have a Configuration");
            }
            if (userRepository == null)
            {
                throw new InvalidDataException("AuthService must have a AuthUserRepository");
            }

            _configuration = configuration;
            _userRepository = userRepository;
            _jwtHelper = new JwtHelper(configuration);
        }
        
        public AuthUser FindUser(string username)
        {
            return _userRepository.FindUser(username);
        }
        
        public bool CreateUser(AuthUser user, string registerDtoPassword)
        {
            var salt = _jwtHelper.CreateSalt();
            var newUser = new AuthUser()
            {
                Username = user.Username,
                Email = user.Email,
                Salt = salt,
                HashedPassword = _jwtHelper.HashedPassword(registerDtoPassword, salt)
            };
            return _userRepository.CreateUser(newUser);
        }

        public List<Permission> GetPermissions(int id)
        {
            return _userRepository.GetUserPermissions(id);
        }
        
        public bool DeleteUser(AuthUser user)
        {
            return _userRepository.DeleteUser(user);
        }
        
        public bool AdminDeleteUser(AuthUser user)
        {
            return _userRepository.DeleteUser(user);
        }
        
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

        public JwtToken GenerateJwtToken(AuthUser user, string password)
        {
            return _jwtHelper.GenerateJwtToken(user, password);
        }

        public bool Authenticate(AuthUser user, string password)
        {
            return _jwtHelper.Authenticate(user, password);
        }

        public string HashedPassword(string password, byte[] userSalt)
        {
            return _jwtHelper.HashedPassword(password, userSalt);
        }

        public byte[] CreateSalt()
        {
            return _jwtHelper.CreateSalt();
        }
    }
}