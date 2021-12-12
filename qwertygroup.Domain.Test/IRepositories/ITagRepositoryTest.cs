using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using System.Collections.Generic;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;
using System.Linq;

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

        [Fact]
        public void Update_Tag_Method_Exists_And_Updates_Given_Tag()
        {
            //Setup the mock repository and define methods
            var mock = new Mock<ITagRepository>();
            Tag tag = new Tag { Text = "someTag", Id = 1 };
            var fakeList = new List<Tag> { tag };
            Tag tag2 = new Tag { Text = "someTag2", Id = 1 };
            var repository = mock.Object;
            
            mock.Setup(r => r.GetAllTags()).Returns(fakeList);
            mock.Setup(r => r.UpdateTag(tag2)).Callback(() => fakeList.First(b => b.Id == tag2.Id).Text=tag2.Text);

            repository.UpdateTag(tag2);

            Assert.True(tag.Text==tag2.Text);
        }
    }
}