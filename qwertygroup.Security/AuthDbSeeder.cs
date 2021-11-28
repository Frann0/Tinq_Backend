using System.Text;
using qwertygroup.Security.Entities;

namespace qwertygroup.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext _ctx;
        private readonly ISecurityService _securityService;

        public AuthDbSeeder(AuthDbContext ctx,
            ISecurityService securityService)
        {
            _ctx = ctx;
            _securityService = securityService;
        }
        
        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var salt = "#)(&€=&)adljfh";
            _ctx.AuthUsers.Add(new AuthUserEntity
            {
                Salt = salt,
                HashedPassword = _securityService.HashedPassword(
                    "demo123", 
                    Encoding.ASCII.GetBytes(salt)),
                    Username = "jbn"
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