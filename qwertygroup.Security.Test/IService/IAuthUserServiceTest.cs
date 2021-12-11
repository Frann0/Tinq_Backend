using System.Linq;
using Moq;
using Xunit;

namespace qwertygroup.Security.Test.IService
{
    public class IAuthUserServiceTest
    {
        private Mock<IServices.IAuthUserService> _service;

        public IAuthUserServiceTest()
        {
            _service = new Mock<IServices.IAuthUserService>();
        }
        
        [Fact]
        public void IAuthUserService_CanBeInstantiated(){
            var mockService = new Mock<IServices.IAuthUserService>();
            Assert.NotNull(mockService);
        }
        
        [Fact]
        public void IAuthService_HasGenerateJwtTokenMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "GenerateJwtToken".Equals(m.Name));
            Assert.NotNull(method);
        }
    }
}