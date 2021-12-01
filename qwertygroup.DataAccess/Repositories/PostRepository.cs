using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.Models;
using qwertygroup.DataAccess.Entities;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PostContext _context;

        public PostRepository(PostContext context)
        {
            _context = context;
        }
        public IEnumerable<Post> GetAllPosts()
        {
            var joinBodyQuery = from post in _context.posts
                                join body in _context.bodies on post.BodyId equals body.Id into bodyq
                                from subbody in bodyq.DefaultIfEmpty()
                                select new
                                {
                                    post.Id,
                                    post.UserId,
                                    post.TitleId,
                                    post.BodyId,
                                    Body = subbody.Text ?? string.Empty
                                };

            var joinTitleQuery = from post in joinBodyQuery
                                 join title in _context.titles on post.TitleId equals title.Id into titleq
                                 from subtitle in titleq.DefaultIfEmpty()
                                 select new
                                 {
                                     post.Id,
                                     post.UserId,
                                     post.Body,
                                     post.BodyId,
                                     post.TitleId,
                                     Title = subtitle.Text ?? string.Empty
                                 };

            return joinTitleQuery.Select(p => new Post
            {
                Id = p.Id,
                UserId = p.UserId,
                Body = p.Body,
                BodyId = p.BodyId,
                Title = p.Title,
                TitleId = p.TitleId
            });
        }
    }
}