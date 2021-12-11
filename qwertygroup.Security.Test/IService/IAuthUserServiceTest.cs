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
        public void IAuthService_HasFindUserMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "FindUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasCreateUserMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "CreateUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasDeleteUserMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "DeleteUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasAdminDeleteUserMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "AdminDeleteUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasGetAllUsersMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "GetAllUsers".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthService_HasGetPermissionsMethod()
        {
            var method = typeof(IServices.IAuthUserService)
                .GetMethods().FirstOrDefault(m => "GetPermissions".Equals(m.Name));
            Assert.NotNull(method);
        }
    }
}