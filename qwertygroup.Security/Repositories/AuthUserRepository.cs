using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public bool DeleteUser(AuthUser user)
        {
            var result = _authDbContext.AuthUsers.Remove(user);
            return true;
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

        public IEnumerable<UserPermission> GetAllUserPermissions()
        {
            var query = from userPermission in _authDbContext.UserPermissions
                        join user in _authDbContext.AuthUsers on userPermission.UserId equals user.Id
                        select new UserPermission
                        {
                            UserId=userPermission.UserId,
                            User=user,
                            PermissionId=userPermission.PermissionId
                        };
            var query2 =    from userPermission in query
                            join permission in _authDbContext.Permissions on userPermission.PermissionId equals permission.Id
                            select new UserPermission{
                                UserId=userPermission.UserId,
                                User=userPermission.User,
                                PermissionId=userPermission.PermissionId,
                                Permission=permission                                
                            };
            return query2;    

        }

        // TODO UpdateUser

        // TODO AddPermission

        // TODO RemovePermission
    }
}