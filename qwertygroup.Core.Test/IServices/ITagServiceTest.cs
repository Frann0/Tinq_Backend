using Xunit;
using Moq;
using qwertygroup.Core.Models;
using System.Collections.Generic;
using qwertygroup.Core.IServices;

namespace qwertygroup.Core.Test.IServices
{
    public class ITagServiceTest
    {
        [Fact]
        public void ITagServiceExists(){
            ITagService tagService = new Mock<ITagService>().Object;
            Assert.NotNull(tagService);
        }

        public void GetAllTagsMethodExistsAndReturnsListOfTags(){
            var mock = new Mock<ITagService>();
            var fakeList = new List<Tag>();
            mock.Setup(r=>r.GetAllTags()).Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList,service.GetAllTags());
        }

        [Fact]
        public void ITagRepository_Has_CreateTags_Method_And_Returns_CreatedTag()
        {
            Mock<ITagService> mock = new Mock<ITagService>();
            Tag newTag = new Tag { Text = "new Tag" };
            mock.Setup(r => r.CreateTag(newTag)).Returns(newTag);
            Tag resultTag = mock.Object.CreateTag(newTag);
            Assert.IsAssignableFrom<Tag>(resultTag);
        }
    }
}