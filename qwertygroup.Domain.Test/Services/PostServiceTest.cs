using System;
using System.Collections.Generic;
using Moq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;
using qwertygroup.Domain.Services;
using Xunit;

namespace qwertygroup.Domain.Test.Services
{
    public class PostServiceTest
    {
        private readonly IPostService _postService;
        private readonly Mock<IPostRepository> _mock;
        private readonly IPostRepository _mockPostRepository;
        private List<Post> _expected = new List<Post>{new Post{Id = 0, TitleId=0,UserId=0,BodyId=0},
                                                      new Post{Id = 1, TitleId=1,UserId=1,BodyId=1}};

        public PostServiceTest(){
            _mock = new Mock<IPostRepository>();
            _mockPostRepository=_mock.Object;
            _postService = new PostService(_mockPostRepository);
        }

        [Fact]
        public void PostService_Exists_And_Extends_IPostService(){
            IPostService ps = new PostService(_mockPostRepository);
            Assert.NotNull(ps);
            Assert.IsAssignableFrom<IPostService>(ps);
        }

        [Fact]
        public void PostService_With_Null_Parameter_Throws_Exception(){
            IPostService ps;
            Assert.Equal(
            Assert.Throws<MissingFieldException>(()=> ps = new PostService(null)).Message,
            "PostService must hate a PostRepository!");
        }

        [Fact]
        public void PostService_Has_GetAllPostsMethod_And_Returns_List_Of_Posts(){
            _mock.Setup(r=>r.GetAllPosts()).Returns(_expected);
            Assert.Equal(_postService.GetAllPosts(),_expected);
        }
    }
}