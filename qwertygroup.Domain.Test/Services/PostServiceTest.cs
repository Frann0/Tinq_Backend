using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.DataAccess.Entities;
using qwertygroup.Domain.IRepositories;
using qwertygroup.Domain.Services;
using Xunit;

namespace qwertygroup.Domain.Test.Services
{
    public class PostServiceTest
    {
        private readonly IPostService _postService;
        private readonly Mock<IPostRepository> _mock;
        private readonly Mock<IPostTagRepository> _postTagMock;
        private readonly IPostRepository _mockPostRepository;
        private readonly IPostTagRepository _mockPostTagRepository;
        private readonly Post _expectedPost = new Post { TitleId = 1231231, BodyId = 1337 };
        private List<Post> _expected = new List<Post>{new Post{Id = 1, TitleId=0,UserId=0,BodyId=0},
                                                      new Post{Id = 2, TitleId=1,UserId=1,BodyId=1}};

        public PostServiceTest()
        {
            _mock = new Mock<IPostRepository>();
            _postTagMock = new Mock<IPostTagRepository>();
            _mock.Setup(r => r.GetAllPosts()).Returns(_expected);
            _mock.Setup(r => r.CreatePost(_expectedPost)).Callback(() => _expected.Add(_expectedPost)).Returns(_expectedPost);
            _mock.Setup(r => r.DeletePost(_expected[0])).Callback(() => _expected.Remove(_expected[0]));
            _mockPostRepository = _mock.Object;
            _mockPostTagRepository = _postTagMock.Object;
            _postService = new PostService(_mockPostRepository, _mockPostTagRepository);
        }

        [Fact]
        public void PostService_Exists_And_Extends_IPostService()
        {
            IPostService ps = new PostService(_mockPostRepository, _mockPostTagRepository);

            Assert.NotNull(ps);
            Assert.IsAssignableFrom<IPostService>(ps);
        }

        [Fact]
        public void PostService_With_Null_Parameter_Throws_Exception()
        {
            IPostService ps;
            Assert.Equal(
            Assert.Throws<MissingFieldException>(() => ps = new PostService(null, null)).Message,
            "PostService must have a PostRepository!");
        }

        [Fact]
        public void PostService_Has_GetAllPostsMethod_And_Returns_List_Of_Posts()
        {
            _mock.Setup(r => r.GetAllPosts()).Returns(_expected);

            Assert.Equal(_postService.GetAllPosts(), _expected);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void PostService_Has_GetPostMethod_And_Returns_TheCorrectPost(int id)
        {
            Assert.Equal(_postService.GetPost(id), _expected[id - 1]);
        }

        [Fact]
        public void PostService_HasCreatePostMethod_And_Returns_TheCreatedPost()
        {
            Assert.Equal(_postService.CreatePost(_expectedPost), _expectedPost);
        }

        [Fact]
        public void PostService_HasDeletePostMethod()
        {
            int count = _expected.Count;
            _postService.DeletePost(_expected[0]);
            Assert.True(count > _expected.Count);
        }

        [Fact]
        public void PostService_HasCreatePostTagRelation_Method()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();

            mock.Setup(r => r.CreatePostTagRelationship(1, 1));
        }

        [Fact]
        public void PostService_Has_RemoveTagFromPost_Method()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();

            mock.Setup(r => r.RemoveTagFromPost(1, 1));
        }

        [Fact]
        public void PostService_has_RemovePostTags_Method()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();
            mock.Setup(r => r.RemovePostTags(1));
        }
    }
}