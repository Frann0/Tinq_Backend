using System;
using System.Collections.Generic;
using System.Linq;
using qwertygroup.Security.Models;
using Xunit;

namespace qwertygroup.Security.Test.Entities
{
    public class AuthUserTest
    {
        private AuthUser _authUser;

        public AuthUserTest()
        {
            _authUser = new AuthUser();
        }

        [Fact]
        public void AuthUser_CanBeInitialized()
        {
            var authUser = new AuthUser();
            Assert.NotNull(authUser);
        }

        [Fact]
        public void AuthUser_Id_MustBeInt()
        {
            Assert.True(_authUser.Id is int);
        }

        [Fact]
        public void AuthUser_SetId_StoresId()
        {
            _authUser.Id = 1;
            Assert.Equal(1, _authUser.Id);
        }
        
        [Fact]
        public void AuthUser_SetUsername_StoresUsername()
        {
            _authUser.Username = "Test";
            Assert.Equal("Test", _authUser.Username);
        }

        [Fact]
        public void AuthUser_SetHashedPassword_StoresHashedPassword()
        {
            _authUser.HashedPassword = "test";
            Assert.Equal("test", _authUser.HashedPassword);
        }

        [Fact]
        public void AuthUser_SetSalt_StoresSalt()
        {
            byte[] testBytes = {0x20, 0x01, 0x24};
            _authUser.Salt = testBytes;
            Assert.Equal(testBytes, _authUser.Salt);
        }

        [Fact]
        public void AuthUser_SetPermission_StoresPermission()
        {
            var testList = new List<Permission>{new Permission(){Id = 1, Name = "Test"}};
            _authUser.Permissions = testList;
            Assert.Equal(testList, _authUser.Permissions);
        }

        [Fact]
        public void AuthUser_SetEmail_StoresEmail()
        {
            _authUser.Email = "test";
            Assert.Equal("test", _authUser.Email);
        }
    }
}