using qwertygroup.Security.Models;
using Xunit;

namespace qwertygroup.Security.Test.Entities
{
    public class UserPermissonTest
    {
        private UserPermission _userPermission;

        public UserPermissonTest()
        {
            _userPermission = new UserPermission();
        }

        [Fact]
        public void UserPermission_CanBeInitialized()
        {
            var userPermission = new UserPermission();
            Assert.NotNull(userPermission);
        }
        
        [Fact]
        public void UserPermission_PermissionId_MustBeInt()
        {
            Assert.True(_userPermission.PermissionId is int);
        }
        
        [Fact]
        public void UserPermission_UserId_MustBeInt()
        {
            Assert.True(_userPermission.UserId is int);
        }
        
        [Fact]
        public void UserPermission_PermissionId_SetPermissionIdStoresId()
        {
            _userPermission.PermissionId = 1;
            Assert.Equal(1, _userPermission.PermissionId);
        }
        
        [Fact]
        public void UserPermission_UserId_SetUserIdStoresId()
        {
            _userPermission.UserId = 1;
            Assert.Equal(1, _userPermission.UserId);
        }

        [Fact]
        public void UserPermission_AuthUser_SetAuthUserStoresAuthUser()
        {
            var user = new AuthUser() {Id = 1, Username = "test"};
            _userPermission.User = user;
            Assert.Equal(user, _userPermission.User);
        }
        
        [Fact]
        public void UserPermission_Permission_SetPermissionStoresPermission()
        {
            var permission = new Permission() {Id = 1, Name = "test"};
            _userPermission.Permission = permission;
            Assert.Equal(permission, _userPermission.Permission);
        }
    }
}