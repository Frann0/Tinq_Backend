using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using qwertygroup.Security.Entities;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly AuthDbContext _authDbContext;
        private const int REGISTERED_USER_PERMISSION_ID = 1;
        private const int ADMIN_USER_PERMISSION_ID = 2;
        public AuthUserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public AuthUser FindUser(string email)
        {
            var authUserEntity = _authDbContext.AuthUsers.FirstOrDefault(user =>
                user.Email.Equals(email));
            
            if (authUserEntity == null) return null;

            return new AuthUser()
            {
                Id = authUserEntity.Id,
                Email = authUserEntity.Email,
                Username = authUserEntity.Username,
                HashedPassword = authUserEntity.HashedPassword,
                Salt = authUserEntity.Salt,
                Permissions = GetUserPermissions(authUserEntity.Id)
            };
        }

        public List<Permission> GetUserPermissions(int id)
        {
            return _authDbContext.UserPermissions.Include(up => up.Permission)
                .Where(up => up.UserId == id)
                .Select(up => up.Permission)
                .ToList();
        }
        public List<AuthUser> GetAllUsers2()
        {
            return _authDbContext.AuthUsers.Select(u => new AuthUser()
            {
                Username = u.Username,
                Email = u.Email,
                Id = u.Id
            }).ToList();
        }

        public List<AuthUser> GetAllUsers()
        {
            var userPermissions = _authDbContext.UserPermissions
                .Include(up => up.User)
                .Where(up => up.UserId == up.User.Id)
                .Include(up => up.Permission)
                .Where(up => up.PermissionId == up.Permission.Id)
                .ToList();

            var usersWithPermissions = new List<AuthUser>();

            var dictionary = new Dictionary<AuthUser, List<Permission>>();

            foreach (var up in userPermissions)
            {
                if (!dictionary.ContainsKey(up.User))
                {
                    var user = up.User;
                    var permission = new List<Permission>(){up.Permission};
                    dictionary.Add(user, permission);
                }
            }

            foreach (var up in userPermissions)
            {
                dictionary.FirstOrDefault(u => u.Key.Id == up.UserId).Value.Add(up.Permission);
            }

            foreach (var d in dictionary)
            {
                var u = d.Key;
                var p = d.Value.Distinct().ToList();
                u.Permissions.AddRange(p);
            }

            return usersWithPermissions;

        }
        

        public bool DeleteUser(AuthUser user)
        {
            var result = _authDbContext.AuthUsers.Remove(user);
            return result != null;
        }

        public bool CreateUser(AuthUser newUser)
        {
            var user = new AuthUser()
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Salt = newUser.Salt,
                HashedPassword = newUser.HashedPassword
            };
            
            _authDbContext.AuthUsers.Add(user);
            
            return _authDbContext.SaveChanges() > 0;
        }

        

        // TODO UpdateUser
        
        // TODO AddPermission
        
        // TODO RemovePermission
    }
}