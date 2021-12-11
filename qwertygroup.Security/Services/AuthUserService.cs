using System.Collections.Generic;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthUserService(IAuthUserRepository authUserRepository,
            IAuthService authService)
        {
            _userRepository = authUserRepository;
            _authService = authService;
        }
        
        public AuthUser FindUser(string username)
        {
            return _userRepository.FindUser(username);
        }

        public bool CreateUser(AuthUser user, string password)
        {
            var salt = _authService.CreateSalt();
            var newUser = new AuthUser()
            {
                Username = user.Username,
                Email = user.Email,
                Salt = salt,
                HashedPassword = _authService.HashedPassword(password, salt)
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
    }
}