using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext _ctx;
        private readonly IAuthService _authService;

        public AuthDbSeeder(AuthDbContext ctx,
            IAuthService securityService)
        {
            _ctx = ctx;
            _authService = securityService;
        }

        public void SeedDevelopment()
        {
            
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            var password = "Pa$$w0rd";
            var salt = _authService.CreateSalt();
            
            var authUser = new AuthUser()
            {
                Username = "usr12047019275",
                Email = "j@j.dk",
                Salt = salt,
                HashedPassword = _authService.HashedPassword(password, salt)
            };
            
            var authUser2 = new AuthUser()
            {
                Username = "usr128178509",
                Email = "b@b.dk",
                Salt = salt,
                HashedPassword = _authService.HashedPassword(password, salt)
            };
            
            _ctx.AuthUsers.Add(authUser);
            _ctx.AuthUsers.Add(authUser2);
            _ctx.SaveChanges();
            _ctx.Permissions.AddRange(new Permission()
            {
                Name = "RegisteredUser"
            }, new Permission()
            {
                Name = "Admin"
            });
            _ctx.UserPermissions.AddRange(
                new UserPermission()
                {
                    PermissionId = 1, 
                    UserId = 1
                },
                new UserPermission()
                {
                    PermissionId = 2, 
                    UserId = 1
                },
                new UserPermission()
                {
                    PermissionId = 2,
                    UserId = 2
                });
            _ctx.SaveChanges();
        }
        

        public void SeedProduction()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }
        
    }
}