
using Microsoft.EntityFrameworkCore;
using qwertygroup.DataAccess.Entities;

namespace qwertygroup.DataAccess
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> opt) : base(opt){}

        public DbSet<BodyEntity> bodies { get; set; }
        public DbSet<TitleEntity> titles { get; set; }
        public DbSet<PostEntity> posts{get;set;}
        public DbSet<TagEntity> tags{get;set;}
        public DbSet<PostTagEntity> postTags{get;set;}
    }
}