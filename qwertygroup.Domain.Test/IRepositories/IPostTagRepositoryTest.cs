using Xunit;
using Moq;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.Domain.Test.IRepositories
{
    public class IPostTagRepositoryTest
    {
        [Fact]
        public void IPostTagRepositoryExists()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();
            Assert.NotNull(mock.Object);
        }

        [Fact]
        public void IPostTagRepository_Has_CreatePostTagRelationship_Method()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();
            mock.Setup(r => r.CreatePostTagRelationship(1, 1));
        }
        [Fact]
        public void IPostTagRepository_Has_RemoveTagFromPost_Method()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();
            mock.Setup(r => r.RemoveTagFromPost(1, 1));
        }
        [Fact]
        public void IPostTagRepository_Has_RemovePostTags_Method()
        {
            Mock<IPostTagRepository> mock = new Mock<IPostTagRepository>();
            mock.Setup(r => r.RemovePostTags(1));
        }
    }
}