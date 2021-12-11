using System.Linq;
using Moq;
using qwertygroup.Security.IAuthUserService;
using Xunit;

namespace qwertygroup.Security.Test.IService
{
    public class IAuthServiceTest
    {
        private Mock<IAuthService> _service;

        public IAuthServiceTest()
        {
            _service = new Mock<IAuthService>();
        }
        
        [Fact]
        public void IAuthUserService_CanBeInstantiated(){
            var mockService = new Mock<IAuthService>();
            Assert.NotNull(mockService);
        }
        
        [Fact]
        public void IAuthService_HasGenerateJwtTokenMethod()
        {
            var method = typeof(IAuthService)
                .GetMethods().FirstOrDefault(m => "GenerateJwtToken".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasHashedPasswordMethod()
        {
            var method = typeof(IAuthService)
                .GetMethods().FirstOrDefault(m => "HashedPassword".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasCreateSaltMethod()
        {
            var method = typeof(IAuthService)
                .GetMethods().FirstOrDefault(m => "CreateSalt".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasAuthenticateMethod()
        {
            var method = typeof(IAuthService)
                .GetMethods().FirstOrDefault(m => "Authenticate".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasAssignAdminPermissionToUserMethod()
        {
            var method = typeof(IAuthService)
                .GetMethods().FirstOrDefault(m => "AssignAdminPermissionToUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasRemoveAdminPermissionFromUserMethod()
        {
            var method = typeof(IAuthService)
                .GetMethods().FirstOrDefault(m => "RemoveAdminPermissionFromUser".Equals(m.Name));
            Assert.NotNull(method);
        }
    }
}