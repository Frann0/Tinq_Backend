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
        private readonly IPostRepository _mockPostRepository;

        public PostService(IPostRepository mockPostRepository)
        {
           if(mockPostRepository==null)
           throw new System.MissingFieldException("PostService must hate a PostRepository!");
            _mockPostRepository = mockPostRepository;
        }

        public List<Post> GetAllPosts()
        {
            return _mockPostRepository.GetAllPosts().ToList();
        }
    }
}