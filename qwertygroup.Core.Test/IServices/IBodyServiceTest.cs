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
    }
}