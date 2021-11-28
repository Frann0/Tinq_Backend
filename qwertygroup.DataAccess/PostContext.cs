
using Microsoft.EntityFrameworkCore;
using qwertygroup.DataAccess.Entities;

namespace qwertygroup.DataAccess
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<BodyEntity> bodies { get; set; }

    }
}