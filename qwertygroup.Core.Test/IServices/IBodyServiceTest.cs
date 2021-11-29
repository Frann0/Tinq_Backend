using Xunit;
using Moq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using System.Collections.Generic;

namespace qwertygroup.Core.Test.IServices
{
    public class IBodyServiceTest
    {

        [Fact]
        public void IBodyService_IsAvailable(){
            var service = new Mock<IBodyService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetBody_WithNoParam_ReturnsListOfAllBodies()
        {
            var mock = new Mock<IBodyService>();
            var fakeList = new List<Body>();
            mock.Setup(s => s.GetBodies())
                .Returns(fakeList);
            var service = mock.Object;
            Assert.Equal(fakeList, service.GetBodies());
        }

        [Fact]
        public void Create_Body_Method_Exists_And_Returns_The_Created_Body()
        {
            var mock = new Mock<IBodyService>();
            Body body = new Body { Text = "someBody" };
            mock.Setup(r => r.CreateBody(body.Text))
            .Returns(body);
            var service = mock.Object;
            Assert.Equal(body, service.CreateBody(body.Text));
        }

        [Fact]
        public void Delete_Body_Method_Exists()
        {
            var mock = new Mock<IBodyService>();
            Body body = new Body { Text = "someBody" };
            mock.Setup(r => r.DeleteBody(body.Id));
        }

        [Fact]
        public void Update_Body_Method_Exists()
        {
            var mock = new Mock<IBodyService>();
            Body body = new Body { Text = "someBody" };
            mock.Setup(r => r.UpdateBody(body));
        }

    }
}