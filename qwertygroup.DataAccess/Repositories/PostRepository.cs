using System;
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

        /*
            Returns an IEnumerable of posts that are fully joined with bodies, titles and tags.
            Empty fields are filled with empty strings.
        */
        public IEnumerable<Post> GetAllPosts()
        {
            //Joins post with body
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

            //Joins title with body
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
            //Converts the anonymous type to Post
            List<Post> posts = joinTitleQuery.Select(p => new Post
            {
                Id = p.Id,
                UserId = p.UserId,
                Body = p.Body,
                BodyId = p.BodyId,
                Title = p.Title,
                TitleId = p.TitleId
            }).ToList();

            foreach (Post post in posts)
            {
                //This could be done in the method
                post.Tags = GetTagsForPost(post);
            }
            return posts;
        }

        //Gets the tags for a given post
        public List<Tag> GetTagsForPost(Post post)
        {
            List<Tag> tags = new List<Tag>();
            try
            {
                //Gets matches in ID, should only be one
                var postSubset = _context.posts.Where(t => t.Id == post.Id);
                // Joins the post table with posts tags with the same post id
                var joinPostWithPostTag = from qpost in postSubset
                                          join postTag in _context.postTags on qpost.Id equals postTag.PostId
                                          select new
                                          {
                                              post.Id,
                                              tagId = postTag.TagId
                                          };
                //Join tagIds with postTagIds
                var joinPostTagWithTag = from qpost in joinPostWithPostTag
                                         join tag in _context.tags on qpost.tagId equals tag.Id
                                         select new
                                         {
                                             qpost.Id,
                                             qpost.tagId,
                                             tagText = tag.Text
                                         };
                //Convert the table to a list of tags, since that is what we want.
                var list = joinPostTagWithTag.ToList();
                if (list.Count > 0)
                    tags = list.Select(t => new Tag { Id = t.tagId, Text = t.tagText }).ToList();
            }
            catch (ArgumentNullException)
            {
                //No tags with given post id, so we just return the previously defined empty list of tags
            }
            return tags;
        }

        public Post CreatePost(Post post)
        {
            PostEntity postEntity = new PostEntity(post);
            _context.posts.Add(postEntity);
            _context.SaveChanges();
            post.Id = postEntity.Id;
            return post;
        }

        public void DeletePost(Post post)
        {
            PostEntity postEntity = _context.posts.First(p => p.Id == post.Id);
            _context.posts.Remove(postEntity);
            _context.SaveChanges();
        }
    }
}