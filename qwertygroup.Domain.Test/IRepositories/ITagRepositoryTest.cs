using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using System.Collections.Generic;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.Domain.Test.IRepositories
{
    public class ITagRepositoryTest
    {
        [Fact]
        public void ITagRepositoryExists()
        {
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            Assert.NotNull(mock.Object);
        }
[Fact]
        public void ITagRepository_Has_GetAllTags_Method_And_Returns_ListOfTags()
        {

            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            List<Tag> tags = new List<Tag>();
            mock.Setup(r => r.GetAllTags()).Returns(tags);
            Assert.IsAssignableFrom<List<Tag>>(mock.Object.GetAllTags());
        }
[Fact]
        public void ITagRepository_Has_CreateTags_Method_And_Returns_CreatedTag()
        {
            Mock<ITagRepository> mock = new Mock<ITagRepository>();
            Tag newTag = new Tag { Text = "new Tag" };
            mock.Setup(r => r.CreateTag(newTag)).Returns(newTag);
            Tag resultTag = mock.Object.CreateTag(newTag);
            Assert.IsAssignableFrom<Tag>(resultTag);
        }
    }
}