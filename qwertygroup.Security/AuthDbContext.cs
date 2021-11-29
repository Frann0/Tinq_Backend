using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qwertygroup.Security.Entities;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        public DbSet<AuthUserEntity> AuthUsers { get; set; }
    }
}