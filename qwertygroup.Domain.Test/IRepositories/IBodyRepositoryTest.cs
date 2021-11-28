using Xunit;
using Moq;
using qwertygroup.Domain.Services;
using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.Test.IRepositories
{
    public class IBodyRepositoryTest
    {

        [Fact]
        public void IBodyRepository_IsAvailable(){
            var service = new Mock<IBodyRepository>().Object;
            Assert.NotNull(service);
        }
        
        public void GetBody_WithNoParam_ReturnsListOfAllBodies()
        {
            var mock = new Mock<IBodyRepository>();
            var fakeList = new List<Body>();
            mock.Setup(r => r.GetBodies())
                .Returns(fakeList);
            var repository = mock.Object;
            Assert.Equal(fakeList, repository.GetBodies());
        }
    }
}