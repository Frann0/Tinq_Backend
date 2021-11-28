using Microsoft.EntityFrameworkCore;
using qwertygroup.Security.Entities;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        public DbSet<AuthUserEntity> AuthUsers { get; set; }
    }
}