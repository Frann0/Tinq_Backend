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
        public List<AuthUser> GetAllUsers()
        {
            return _authDbContext.AuthUsers.Select(u => new AuthUser()
            {
                Username = u.Username,
                Email = u.Email,
                Id = u.Id
            }).ToList();
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