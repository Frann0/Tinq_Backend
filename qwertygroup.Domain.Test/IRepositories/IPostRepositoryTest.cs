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
    }
}