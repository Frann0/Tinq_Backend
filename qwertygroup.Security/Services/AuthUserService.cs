using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _userRepository;

        public AuthUserService(IAuthUserRepository authUserRepository)
        {
            _userRepository = authUserRepository;
        }
        
        public AuthUser FindUser(string username)
        {
            return _userRepository.FindUser(username);
        }

        public bool CreateUser(AuthUser user, string password)
        {
            var salt = CreateSalt();
            var newUser = new AuthUser()
            {
                Username = user.Username,
                Email = user.Email,
                Salt = salt,
                HashedPassword = HashedPassword(password, salt)
            };
            return _userRepository.CreateUser(newUser);
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

        public List<Permission> GetPermissions(int id)
        {
            return _userRepository.GetUserPermissions(id);
        }
        
        public byte[] CreateSalt()
        {
            // 128-bit salt
            var rng = new RNGCryptoServiceProvider();
            var salt = new byte[128 / 8];
            rng.GetNonZeroBytes(salt);

            return salt;
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