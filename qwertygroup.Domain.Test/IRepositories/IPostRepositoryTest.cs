using Xunit;
using Moq;
using qwertygroup.Domain.IRepositories;
using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.Test.IRepositories
{
    public class IPostRepositoryTest
    {
        [Fact]
        public void IPostRepository_Exists(){
            var mockRepository = new Mock<IPostRepository>();
            Assert.NotNull(mockRepository);
        }

        [Fact]
        public void IPostRepository_Has_GetAllPostsMethod(){
            var mockRepository = new Mock<IPostRepository>();
            IEnumerable<Post> fakeList  = new List<Post>();
            mockRepository.Setup(r=>r.GetAllPosts()).Returns(fakeList);
            Assert.IsAssignableFrom<IEnumerable<Post>>(mockRepository.Object.GetAllPosts());
        }

        [Fact]
        public void IPostRepository_Has_GetPostMethod(){
            var mockRepository = new Mock<IPostRepository>();
            Post fakePost = new Post{Id=1};
            mockRepository.Setup(r=>r.CreatePost(fakePost)).Returns(fakePost);
            Assert.Equal(fakePost,mockRepository.Object.CreatePost(fakePost));
        }

       [Fact]
        public void IPostRepository_Has_DeletePostMethod(){
            var mockRepository = new Mock<IPostRepository>();
            Post fakePost = new Post{Id=1};
            mockRepository.Setup(r=>r.DeletePost(fakePost));
        }
    }
}