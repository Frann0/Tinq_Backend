using qwertygroup.Security.Models;
using Xunit;

namespace qwertygroup.Security.Test.Entities
{
    public class PermissionTest
    {
        private Permission _permission;

        public PermissionTest()
        {
            _permission = new Permission();
        }

        [Fact]
        public void Permission_CanBeInitialized()
        {
            var permission = new Permission();
            Assert.NotNull(permission);
        }
        
        [Fact]
        public void Body_Id_MustBeInt()
        {
            Assert.True(_permission.Id is int);
        }
        
        [Fact]
        public void Body_SetId_StoresId()
        {
            _permission.Id = 1;
            Assert.Equal(1, _permission.Id);
        }

        [Fact]
        public void Permission_SetName_StoresName()
        {
            _permission.Name = "test";
            Assert.Equal("test", _permission.Name);
        }
    }
}