
using Xunit;
using Moq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;

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
        public void IPostService_Has_GetAllPosts_Method()
        {
            var mockService = new Mock<IPostService>();
            mockService.Setup(r => r.GetAllPosts());
        }

        [Fact]
        public void IPostService_Has_GetPost_Method()
        {
            var mockService = new Mock<IPostService>();
            mockService.Setup(r => r.GetPost(1));
        }
        [Fact]
        public void IPostService_Has_DeletePost_Method()
        {
            var mockService = new Mock<IPostService>();
            mockService.Setup(r => r.DeletePost(new Post { }));
        }

        
        [Fact]
        public void IPostService_Has_CreatePost_Method(){
            var mockService = new Mock<IPostService>();
            mockService.Setup(r=>r.CreatePost(new Post{}));
        }
    }
}