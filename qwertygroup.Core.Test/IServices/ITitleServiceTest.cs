using System.Collections.Generic;
using Moq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using Xunit;

namespace qwertygroup.Core.Test.IServices
{
    public class ITitleServiceTest
    {
        [Fact]
        public void ITitleService_IsAvailable(){
            var service = new Mock<ITitleService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetTitle_WithNoParam_ReturnsListOfAllTitles()
        {
            var mock = new Mock<ITitleService>();
            var fakeList = new List<Title>();
            mock.Setup(s => s.GetTitles())
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.GetTitles());
        }

        [Fact]
        public void Create_Title_Method_Exists_And_Returns_The_Created_Title()
        {
            var mock = new Mock<ITitleService>();
            Title title = new Title { Text = "someTitle" };
            mock.Setup(r => r.CreateTitle(title.Text))
            .Returns(title);
            var service = mock.Object;
            Assert.Equal(title, service.CreateTitle(title.Text));
        }

        
        [Fact]
        public void Delete_Title_Method_Exists()
        {
            var mock = new Mock<ITitleService>();
            Body body = new Body { Text = "someBody" };
            mock.Setup(r => r.DeleteTitle(body.Id));
        }
        
        [Fact]
        public void Update_Title_Method_Exists()
        {
            var mock = new Mock<ITitleService>();
            Title title = new Title { Text = "some title" };
            mock.Setup(r => r.UpdateTitle(title));
        }
    }
}