using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using qwertygroup.Core.IServices;
using System.Collections.Generic;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;
using System.Linq;

namespace qwertygroup.Domain.Test.Services
{
    public class TagServiceTest
    {

        public Mock<ITagRepository> _mockTagRepository;
        public Mock<IPostTagRepository> _mockPostTagRepository;
        public ITagService _tagService;
        private List<Tag> _fakeList = new List<Tag>();

        public TagServiceTest()
        {
            _mockTagRepository = new Mock<ITagRepository>();
            _mockPostTagRepository = new Mock<IPostTagRepository>();
            _tagService = new TagService(_mockTagRepository.Object, _mockPostTagRepository.Object);
            _mockTagRepository.Setup(r => r.GetAllTags()).Returns(_fakeList);
        }

        [Fact]
        public void TagServiceExists()
        {
            Assert.IsAssignableFrom<ITagService>(_tagService);
        }

        [Fact]
        public void TagService_WithNoRepository_ThrowsMissingFieldException()
        {
            Assert.True(
                Assert.Throws<System.MissingFieldException>(() => new TagService(null, null)).Message
                ==
                "TagService Must have a TagRepository!");

            Assert.True(
                Assert.Throws<System.MissingFieldException>(() => new TagService(null, _mockPostTagRepository.Object)).Message
                ==
                "TagService Must have a TagRepository!");

            Assert.True(
                Assert.Throws<System.MissingFieldException>(() => new TagService(_mockTagRepository.Object, null)).Message
                ==
                "TagService Must have a PostTagRepository!");
        }

        [Fact]
        public void GetAllTags_CallsTagRepositoriesFindAll_ExactlyOnce()
        {
            _tagService.GetAllTags();

            _mockTagRepository.Verify(r => r.GetAllTags(), Times.Once);
        }

        [Fact]
        public void TagService_Has_CreateTag_Method_That_Returns_NewTag()
        {
            Tag newTag = new Tag { Text = "new Tag" };
            _mockTagRepository.Setup(r => r.CreateTag(newTag)).Returns(new Tag { Text = newTag.Text });
            Tag resultTag = _tagService.CreateTag(newTag);

            Assert.NotNull(resultTag);
            Assert.Equal(newTag.Text, resultTag.Text);
        }

        [Fact]
        public void Update_Tag_Method_Exists_And_Updates_Given_Tag()
        {
            _fakeList.Add(new Tag { Id = 2, Text = "old text" });
            //Setup the mock fakesitory and define methods
            _mockTagRepository.Setup(r => r.GetAllTags()).Returns(_fakeList);
            Tag updatedTag = new Tag { Id = 2, Text = "new text" };

            _mockTagRepository.Setup(r => r.UpdateTag(updatedTag)).Callback(() =>
            {
                _fakeList.First(b => b.Id == updatedTag.Id).Text = updatedTag.Text;
            });
            _tagService.UpdateTag(updatedTag);

            Assert.True(_fakeList.First(b => b.Id == updatedTag.Id).Text == updatedTag.Text);
        }

        [Fact]
        public void Delete_Tag_Method_Exists_And_Deletes_Given_Tag()
        {
            //Setup the mock repository and define methods
            int idToDelete = 1;
            _fakeList = new List<Tag> { new Tag { Id = idToDelete }, new Tag { Id = idToDelete + 1 } };
            _mockTagRepository.Setup(r => r.DeleteTag(idToDelete))
            .Callback(() => _fakeList.RemoveAll(t => t.Id == idToDelete));

            _tagService.DeleteTag(idToDelete);

            Assert.True(_tagService.GetAllTags().Count < 2);
        }
    }
}