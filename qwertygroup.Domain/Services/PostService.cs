using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace qwertygroup.Domain.Services
{
    public class PostService : MyIdentifyable, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostTagRepository _postTagRepository;

        public PostService(IPostRepository postRepository, IPostTagRepository postTagRepository)
        {
            if (postRepository == null)
                throw new MissingFieldException("PostService must have a PostRepository!");
            _postRepository = postRepository;
            if (postTagRepository == null)
                throw new MissingFieldException("PostService must have a PostTagRepository!");
            _postTagRepository = postTagRepository;
        }
        public List<Post> GetAllPosts()
        {
            return _postRepository.GetAllPosts().ToList();
        }

        public Post GetPost(int id)
        {
            CheckId(id);
            return _postRepository.GetAllPosts().First(p => p.Id == id);
        }

        public List<Post> GetPostByUserID(int userId)
        {
            CheckId(userId);
            return _postRepository.GetAllPosts().Where(post => post.UserId == userId).ToList();
        }
        
        public List<Post> GetPostsBySearchString(string query)
        {
            List<Post> all = GetAllPosts();
            List<Post> res = new List<Post>();
            for (int i = 0; i < all.Count; i++)
            {
                for (int j = 0; j < all[i].Tags.Count; j++)
                {
                    if (all[i].Tags[j].Text.ToLower().Equals(query.ToLower()))
                    {
                        res.Add(all[i]);
                    }
                }
            }

            return res;
        }

        public Post CreatePost(Post post)
        {
            return _postRepository.CreatePost(post);
        }

        public void DeletePost(Post post)
        {
            CheckId(post.Id);
            _postRepository.DeletePost(post);
        }

        public void CreatePostTagRelation(int postId, int tagId)
        {
            _postTagRepository.CreatePostTagRelationship(postId, tagId);
        }

        public void RemoveTagFromPost(int tagId, int postId)
        {
            CheckId(tagId);
            CheckId(postId);
            _postTagRepository.RemoveTagFromPost(tagId, postId);
        }

        public void RemovePostTags(int postId)
        {
            CheckId(postId);
            _postTagRepository.RemovePostTags(postId);
        }
    }
}