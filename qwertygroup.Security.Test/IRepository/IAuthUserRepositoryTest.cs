using System.Linq;
using Moq;
using Xunit;
using qwertygroup.Security.IRepositories;

namespace qwertygroup.Security.Test.IRepository
{
    public class IAuthUserRepositoryTest
    {
        private Mock<IAuthUserRepository> _repository;

        public IAuthUserRepositoryTest()
        {
            _repository = new Mock<IAuthUserRepository>();
        }

        [Fact]
        public void IAuthUserRepository_CanBeInstantiated(){
        var mockRepository = new Mock<IAuthUserRepository>();
        Assert.NotNull(mockRepository);
        }

        [Fact]
        public void IAuthUserRepository_HasFindUserMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "FindUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasGetUserPermissionsMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "GetUserPermissions".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasDeleteUserMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "DeleteUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasCreateUserMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "CreateUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasGetAllUserPermissionsUserMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "GetAllUserPermissions".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasGetAllUsersMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "GetAllUsers".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasAssignAdminPermissionToUserMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "AssignAdminPermissionToUser".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void IAuthUserRepository_HasRemoveAdminPermissionFromUserMethod()
        {
            var method = typeof(IAuthUserRepository)
                .GetMethods().FirstOrDefault(m => "RemoveAdminPermissionFromUser".Equals(m.Name));
            Assert.NotNull(method);
        }
    }
}