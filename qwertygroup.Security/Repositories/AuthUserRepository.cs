using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.Repositories
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly AuthDbContext _authDbContext;
        private const int ADMIN_USER_PERMISSION_ID = 1;
        private const int REGISTERED_USER_PERMISSION_ID = 2;
        private readonly Permission AdminPermission;
        private readonly Permission RegisteredUserPermission;
        
        public AuthUserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
            AdminPermission = new Permission()
            {
                Id = ADMIN_USER_PERMISSION_ID,
                Name = "Admin"
            };
            RegisteredUserPermission = new Permission()
            {
                Id = REGISTERED_USER_PERMISSION_ID,
                Name = "RegisteredUser"
            };
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
            var query = _authDbContext.AuthUsers.FirstOrDefault(u => u.Email == user.Email);
            if (query != null)
            {
                var result = _authDbContext.AuthUsers.Remove(query);
                _authDbContext.SaveChanges();
                return result != null;
            }
            return false;
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
            
            var userEntity = _authDbContext.AuthUsers.Add(user).Entity;
            _authDbContext.SaveChanges();
            
            _authDbContext.UserPermissions.Add(new UserPermission() {
                    PermissionId = REGISTERED_USER_PERMISSION_ID, 
                    UserId = userEntity.Id});
            
            return _authDbContext.SaveChanges() > 0;
        }

        public bool AssignAdminPermissionToUser(AuthUser user)
        {
            // REFAC
            _authDbContext.UserPermissions.Add(new UserPermission()
            {
                PermissionId = ADMIN_USER_PERMISSION_ID, 
                UserId = user.Id
            });
            return _authDbContext.SaveChanges() > 0;
        }
        
        public bool RemoveAdminPermissionFromUser(AuthUser user)
        {
            // REFAC
            _authDbContext.UserPermissions.Remove(new UserPermission()
            {
                PermissionId = ADMIN_USER_PERMISSION_ID, 
                UserId = user.Id
            });
            return _authDbContext.SaveChanges() > 0;
        }
        
        public IEnumerable<AuthUser> GetAllUsersWithPermissions()
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

            var query3 = _authDbContext.AuthUsers.ToList();

            foreach (var u in query3)
            {
                var userPermissions = query2.Where(up => up.UserId == u.Id);
                u.Permissions = userPermissions.Select(up => up.Permission).ToList();
            }

            return query3;
        }
    }
}