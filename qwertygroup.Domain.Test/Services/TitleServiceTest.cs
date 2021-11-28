using qwertygroup.Core.IServices;
using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using qwertygroup.Domain.IRepositories;
using qwertygroup.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace qwertygroup.Domain.Test.Services
{
    public class TitleServiceTest
    {
        private readonly Mock<ITitleRepository> _mockTitleRepository;
        private readonly ITitleService _titleService;
        private readonly List<Title> _expected;
        
        private readonly List<Title> _repoList = new List<Title>();

        public TitleServiceTest()
        {
            _mockTitleRepository = new Mock<ITitleRepository>();
            _titleService = new TitleService(_mockTitleRepository.Object);
            _expected = new List<Title>{
                new Title{Id=1,Text="text1"},
                new Title{Id=2,Text="text2"}
            };
        }

        [Fact]
        public void TitleService_IsITitleService()
        {
            Assert.IsAssignableFrom<ITitleService>(_titleService);
        }

        [Fact]
        public void TitleService_With_Null_TitleRepository_Throws_MissingFieldException()
        {
            Assert.True(
                Assert.Throws<System.MissingFieldException>(() => new TitleService(null)).Message
                ==
                "TitleService Must have a TitleRepository!");
        }

        [Fact]
        public void GetBodies_CallsBodyRepositoriesFindAll_ExactlyOnce()
        {
            _titleService.GetTitles();
            _mockTitleRepository.Verify(r => r.GetTitles(), Times.Once);
        }

        [Fact]
        public void TitleService_CreateTitle_AddsTheTitleToTheListOfTitles()
        {
            List<Title> fakeList = new List<Title>();
            foreach (Title title in _expected)
            {
                _mockTitleRepository.Setup(r => r.CreateTitle(title.Text))
                .Returns(title);
                fakeList.Add(_titleService.CreateTitle(title.Text));
            }
            _mockTitleRepository.Setup(r => r.GetTitles()).Returns(fakeList);
            Assert.NotEmpty(_titleService.GetTitles());
            Assert.Equal(_titleService.GetTitles(), _expected);
        }

        
        [Fact]
        public void Delete_Title_Method_Exists_And_Deletes_Given_Title()
        {
            //Setup the mock repository and define methods
            int idToDelete = 1;
            _mockTitleRepository.Setup(r => r.GetTitles()).Returns(_repoList);
            int intialSize = _titleService.GetTitles().Count;
            _mockTitleRepository.Setup(r => r.DeleteTitle(idToDelete)).Callback(() => _repoList.RemoveAll(t => t.Id == idToDelete));

            _titleService.DeleteTitle(idToDelete);

            Assert.True(_titleService.GetTitles().Count < 2);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(3)]
        public void Delete_Title_Method_Throws_Exception_When_Id_Is_Invalid(int idToDelete)
        {
            //Setup the mock repository and define methods
            _mockTitleRepository.Setup(r => r.GetTitles()).Returns(_repoList);
            string message = "";
            if(idToDelete<=0)
                message = "Id must be greater than 0!";
            else
                message = $"No title with given id: {idToDelete}";
            _mockTitleRepository.Setup(r => r.DeleteTitle(idToDelete)).Callback(() =>
            {
                if (idToDelete <= 0)
                {
                    throw new System.InvalidOperationException(message);
                }
                else if (_repoList.RemoveAll(b => b.Id == idToDelete) <= 0)
                    throw new System.InvalidOperationException(message);
            });

            string resMessage = Assert.Throws<System.InvalidOperationException>(() => _titleService.DeleteTitle(idToDelete)).Message;
            Assert.Equal(resMessage,message);
        }

        [Fact]
        public void Update_Title_Method_Exists_And_Updates_Given_Title()
        {
            _repoList.Add(new Title { Id = 2, Text = "old text" });
            //Setup the mock repository and define methods
            _mockTitleRepository.Setup(r => r.GetTitles()).Returns(_repoList);
            Title updatedTitle = new Title { Id = 2, Text = "new text" };
            _mockTitleRepository.Setup(r => r.UpdateTitle(updatedTitle)).Callback(() =>
            {
                _repoList.First(t => t.Id == updatedTitle.Id).Text = updatedTitle.Text;
            });
            _titleService.UpdateTitle(updatedTitle);

            Assert.True(_repoList.First(b => b.Id == updatedTitle.Id).Text == updatedTitle.Text);
        }
    }
}