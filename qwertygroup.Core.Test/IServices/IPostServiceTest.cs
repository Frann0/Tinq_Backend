using Xunit;
using Moq;
using qwertygroup.Core.IServices;

namespace qwertygroup.Core.Test.IServices
{
    public class IPostServiceTest
    {
        [Fact]
        public void IPostService_Exists(){
            var mockService = new Mock<IPostService>();
            Assert.NotNull(mockService.Object);
        }
    }
}