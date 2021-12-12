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
            _context.postTags.Add(
                new PostTagEntity
                {
                    PostId = postId,
                    TagId = tagId
                });
            _context.SaveChanges();
        }

        public void RemoveTagFromPost(int tagId, int postId)
        {
            //Gets PostTags, where postid and tagid matches the given ids
            var query = _context.postTags.Where(pt => pt.PostId == postId && pt.TagId == tagId);
            foreach (var item in query.ToList())
            {
                _context.postTags.Remove(item);
            }
            _context.SaveChanges();
        }

        //Removes all tags from the given post
        public void RemovePostTags(int postId)
        {
            //Gets all posts with the given postid
            var query = _context.postTags.Where(pt => pt.PostId == postId);
            foreach (var item in query.ToList())
            {
                _context.postTags.Remove(item);
            }
            _context.SaveChanges();
        }

    }
}