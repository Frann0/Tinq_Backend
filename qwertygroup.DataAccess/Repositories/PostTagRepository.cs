
using System.Linq;
using qwertygroup.DataAccess.Entities;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.DataAccess.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private readonly PostContext _context;

        public PostTagRepository(PostContext context)
        {
            _context = context;
        }

        public void CreatePostTagRelationship(int tagId, int postId)
        {
            _context.postTags.Add(new PostTagEntity { postId = postId, tagId = tagId });
            _context.SaveChanges();
        }
        public void RemoveTagFromPost(int tagId, int postId)
        {
            var query = _context.postTags.Where(pt => pt.postId == postId && pt.tagId == tagId);
            foreach (var item in query.ToList())
            {
                _context.postTags.Remove(item);
            }
            _context.SaveChanges();
        }

        public void RemovePostTags(int postId)
        {
            var query = _context.postTags.Where(pt => pt.postId == postId);
            foreach (var item in query.ToList())
            {
                _context.postTags.Remove(item);
            }
            _context.SaveChanges();
        }


        
    }
}