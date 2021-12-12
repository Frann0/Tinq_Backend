using Xunit;
using Moq;
using qwertygroup.Domain.IRepositories;
using System.Collections.Generic;
using qwertygroup.Core.Models;
using System.Linq;

namespace qwertygroup.Domain.Test.IRepositories
{
    public class ITitleRepositoryTest
    {
        [Fact]
        public void ITitleRepository_IsAvailable()
        {
            //instantiate a mock repository instance
            var repository = new Mock<ITitleRepository>().Object;
            Assert.NotNull(repository);
        }

        [Fact]
        public void GetBody_WithNoParam_ReturnsListOfAllTitles()
        {
            //Set up mock repository and define get bodies method
            var mock = new Mock<ITitleRepository>();
            var fakeList = new List<Title>();
            mock.Setup(r => r.GetTitles()).Returns(fakeList);

            var repository = mock.Object;
            Assert.Equal(fakeList, repository.GetTitles());
        }

         [Fact]
        public void Create_Title_Method_Exists_And_Returns_The_Created_Title()
        {
            //Set up mock repository and define methods
            var mock = new Mock<ITitleRepository>();
            Title title = new Title { Text = "some title" };
            mock.Setup(r => r.CreateTitle(title.Text)).Returns(title);

            var repository = mock.Object;
            Assert.Equal(title, repository.CreateTitle(title.Text));
        }

        [Fact]
        public void Delete_Title_Method_Exists_And_Deletes_Given_Title()
        {
            //Setup the mock repository and define methods
            var mock = new Mock<ITitleRepository>();
            Title title = new Title { Text = "some title", Id = 1 };
            var fakeList = new List<Title> { title };
            var repository = mock.Object;
            mock.Setup(r => r.GetTitles()).Returns(fakeList);
            mock.Setup(r => r.DeleteTitle(title.Id)).Callback(() => fakeList.RemoveAll(t => t.Id == title.Id));

            repository.DeleteTitle(title.Id);

            Assert.Empty(fakeList);
        }

        [Fact]
        public void Update_Title_Method_Exists_And_Updates_Given_Title()
        {
            //Setup the mock repository and define methods
            var mock = new Mock<ITitleRepository>();
            Title title = new Title { Text = "some title", Id = 1 };
            var fakeList = new List<Title> { title };
            Title title2 = new Title { Text = "some new title", Id = 1 };
            
            var repository = mock.Object;
            mock.Setup(r => r.GetTitles()).Returns(fakeList);
            mock.Setup(r => r.UpdateTitle(title2)).Callback(() 
                => fakeList.First(t => t.Id == title2.Id).Text=title2.Text);

            repository.UpdateTitle(title2);

            Assert.True(title.Text==title2.Text);
        }
    }
}