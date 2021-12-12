using qwertygroup.Core.Models;

namespace qwertygroup.DataAccess.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public int BodyId { get; set; }
        public int TitleId { get; set; }
        public int UserId { get; set; }
        
        public PostEntity(){}

        public PostEntity(Post post)
        {
            Id=post.Id;
            BodyId = post.BodyId;
            TitleId = post.TitleId;
            UserId = post.UserId;
        }
    }
}