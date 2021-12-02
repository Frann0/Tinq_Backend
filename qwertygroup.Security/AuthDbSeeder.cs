using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using qwertygroup.Security.Entities;

namespace qwertygroup.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext _ctx;
        private readonly ISecurityService _securityService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthDbSeeder(AuthDbContext ctx,
            ISecurityService securityService, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _securityService = securityService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public void SeedDevelopment()
        {
            
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            

            var identityUser = new IdentityUser()
            {
                UserName = "jjj",
                Email = "j@j.dk"
            };

            _userManager.CreateAsync(identityUser, "Pa$$w0rd");
        }
        

        public void SeedProduction()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            
        }
        
    }
}