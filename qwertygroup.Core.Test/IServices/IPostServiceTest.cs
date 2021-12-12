using Xunit;
using Moq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using System.Collections.Generic;

namespace qwertygroup.Core.Test.IServices
{
    public class IPostServiceTest
    {
        [Fact]
        public void IPostService_Exists()
        {
            var mockService = new Mock<IPostService>();
            Assert.NotNull(mockService.Object);
        }

        [Fact]
        public void IPostService_Has_GetAllPosts_Method_That_Returns_List_Of_Posts()
        {
            var mockService = new Mock<IPostService>();
            var fakeList = new List<Post>();
            mockService.Setup(r => r.GetAllPosts()).Returns(fakeList);
            Assert.IsAssignableFrom<List<Post>>(mockService.Object.GetAllPosts());
        }

        [Fact]
        public void IPostService_Has_GetPost_Method_And_Returns_Post()
        {
            var mockService = new Mock<IPostService>();
            var fakePost = new Post{Id=1};
            mockService.Setup(r => r.GetPost(1)).Returns(fakePost);
            Assert.IsAssignableFrom<Post>(mockService.Object.GetPost(1));
        }
        [Fact]
        public void IPostService_Has_DeletePost_Method()
        {
            var mockService = new Mock<IPostService>();
            mockService.Setup(r => r.DeletePost(new Post {}));
        }

        
        [Fact]
        public void IPostService_Has_CreatePost_Method_That_Returns_Created_Post(){
            var mockService = new Mock<IPostService>();
            var newPost = new Post{};
            mockService.Setup(r=>r.CreatePost(newPost)).Returns(newPost);
            Assert.Equal(mockService.Object.CreatePost(newPost),newPost);
        }

        [Fact]
        public void IPostService_Has_RemoveTagFromPost_Method(){
            var mockService = new Mock<IPostService>();
            mockService.Setup(r => r.RemoveTagFromPost(1,1));
        }
        [Fact]
        public void IPostService_Has_RemovePostTags_Method(){
            var mockService = new Mock<IPostService>();
            mockService.Setup(r => r.RemovePostTags(1));
        }
    }
}