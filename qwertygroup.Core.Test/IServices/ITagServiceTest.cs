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

        public void GetAllTagsMethodExists_And_ReturnsListOfTags(){
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

        
        
        [Fact]
        public void Delete_Tag_Method_Exists()
        {
            var mock = new Mock<ITagService>();
            Body body = new Body { Text = "someBody" };
            mock.Setup(r => r.DeleteTag(body.Id));
        }
        
        [Fact]
        public void UpdateTag_Method_Exists_And_Returns_UpdatedTag()
        {
            var mock = new Mock<ITagService>();
            Tag tag = new Tag {Id=1, Text = "some tag1" };
            string newTag = "SomeTag2";
            mock.Setup(r => r.UpdateTag(tag)).Callback(()=>tag.Text=newTag).Returns(tag);
            Assert.Equal(mock.Object.UpdateTag(tag),tag);
        }
    }
}