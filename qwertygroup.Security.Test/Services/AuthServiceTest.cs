using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.IServices;
using qwertygroup.Security.Services;
using Xunit;

namespace qwertygroup.Security.Test.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IAuthUserRepository> _mockRepo;
        private readonly AuthService _authService;
        private readonly Mock<IConfiguration> _configuration;

        public AuthServiceTest()
        {
            _configuration = new Mock<IConfiguration>();
            _mockRepo = new Mock<IAuthUserRepository>();
            _authService = new AuthService(_configuration.Object, _mockRepo.Object);
        }
        
        [Fact]
        public void AuthService_Exists()
        {
            IAuthService authService = new AuthService(_configuration.Object, _mockRepo.Object);
            Assert.NotNull(authService);
        }

        [Fact]
        public void AuthService_WithNullParams_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(
                () => new AuthService(null, null));
        }
        
        [Fact]
        public void AuthService_WithNullConfiguration_ThrowsInvalidDataException_WithMessage()
        {
            Assert.Equal(Assert.Throws<InvalidDataException>(
                () => new AuthService(null, _mockRepo.Object))
                .Message, "AuthService must have a Configuration");
        }
        
        [Fact]
        public void AuthService_WithNullRepository_ThrowsInvalidDataException_WithMessage()
        {
            Assert.Equal(Assert.Throws<InvalidDataException>(
                () => new AuthService(_configuration.Object, null))
                .Message, "AuthService must have a AuthUserRepository");
        }

        [Fact]
        public void AuthService_Extends_IAuthService()
        {
            IAuthService authService = new AuthService(_configuration.Object, _mockRepo.Object);
            Assert.IsAssignableFrom<IAuthService>(authService);
        }
        
        [Fact]
        public void AuthService_HasFindUserMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "FindUser".Equals(m.Name));
        }

        [Fact]
        public void AuthService_HasCreateUserMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "CreateUser".Equals(m.Name));
        }
        
        [Fact]
        public void AuthService_HasDeleteUserMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "DeleteUser".Equals(m.Name));
        }
        
        [Fact]
        public void AuthService_HasAdminDeleteUserMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "AdminDeleteUser".Equals(m.Name));
        }
        
        [Fact]
        public void AuthService_HasGetPermissionsMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "GetPermissions".Equals(m.Name));
        }
        
        [Fact]
        public void AuthService_HasGetAllUsersMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "GetAllUsers".Equals(m.Name));
        }
        
        [Fact]
        public void AuthService_HasAssignAdminPermissionToUserMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "AssignAdminPermissionToUser".Equals(m.Name));
        }
        
        [Fact]
        public void AuthService_HasRemoveAdminPermissionFromUserMethod()
        {
            var method = typeof(AuthService).GetMethods().FirstOrDefault(m => "RemoveAdminPermissionFromUser".Equals(m.Name));
        }
    }
}