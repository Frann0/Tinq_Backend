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
        
        
    }
}