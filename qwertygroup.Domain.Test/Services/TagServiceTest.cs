using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using qwertygroup.Core.IServices;
using System.Collections.Generic;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.Domain.Test.Services
{
    public class TagServiceTest
    {
        public Mock<ITagRepository> _mockRepository;
        public ITagService _tagService;
        private readonly List<Tag> _fakeList = new List<Tag>();

        public TagServiceTest()
        {
            _mockRepository = new Mock<ITagRepository>();
            _tagService = new TagService(_mockRepository.Object,null);
            _mockRepository.Setup(r => r.GetAllTags()).Returns(_fakeList);
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
                Assert.Throws<System.MissingFieldException>(() => new TagService(null,null)).Message
                ==
                "TagService Must have a TagRepository!");
        }

        [Fact]
        public void GetAllTags_CallsTagRepositoriesFindAll_ExactlyOnce()
        {
            _tagService.GetAllTags();
            _mockRepository.Verify(r => r.GetAllTags(), Times.Once);
        }

        [Fact]
        public void TagService_Has_CreateTag_Method_That_Returns_NewTag(){
            Tag newTag = new Tag{Text="new Tag"};
            _mockRepository.Setup(r=>r.CreateTag(newTag)).Returns(new Tag{Text=newTag.Text});
            Tag resultTag = _tagService.CreateTag(newTag);
            Assert.NotNull(resultTag);
            Assert.Equal(newTag.Text,resultTag.Text);
        }
    }
}