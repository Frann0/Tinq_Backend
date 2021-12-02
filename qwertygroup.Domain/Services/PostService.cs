using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qwertygroup.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository){
            if(postRepository==null)
                throw new MissingFieldException("PostService must hate a PostRepository!");
            _postRepository=postRepository;
        }
        public List<Post> GetAllPosts()
        {
            return _postRepository.GetAllPosts().ToList();
        }

        public Post GetPost(int id)
        {
           return _postRepository.GetAllPosts().First(p=>p.Id==id);
        }

        public Post CreatePost(Post post)
        {
            return _postRepository.CreatePost(post);
        }

        public void DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
        }
    }
}