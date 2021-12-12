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
        public void ITitleService_IsAvailable()
        {
            var service = new Mock<ITitleService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetTitle_Exists_And_ReturnsListOfAllTitles()
        {
            var mock = new Mock<ITitleService>();
            var fakeList = new List<Title>();
            mock.Setup(s => s.GetTitles())
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.GetTitles());
        }

        [Fact]
        public void CreateTitle_MethodExists_And_Returns_TheCreatedTitle()
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
        public void UpdateTitle_Method_Exists_And_Returns_UpdatedTitle()
        {
            var mock = new Mock<ITitleService>();
            Title title = new Title { Id = 1, Text = "some title1" };
            string newTitle = "SomeTitle2";
            mock.Setup(r => r.UpdateTitle(title)).Callback(() => title.Text = newTitle).Returns(title);
            Assert.Equal(mock.Object.UpdateTitle(title), title);
        }

        [Fact]
        public void GetTitle_Method_Exists_And_ReturnsTitle()
        {
            var mock = new Mock<ITitleService>();
            Title title = new Title { Id = 1, Text = "some title" };
            mock.Setup(r => r.GetTitle(title.Id)
            ).Returns(title);
            Assert.IsType<Title>(mock.Object.GetTitle(title.Id));
        }
    }
}