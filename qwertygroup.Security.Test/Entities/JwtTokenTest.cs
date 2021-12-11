using qwertygroup.Security.Models;
using Xunit;

namespace qwertygroup.Security.Test.Entities
{
    public class JwtTokenTest
    {
        private JwtToken _token;

        public JwtTokenTest()
        {
            _token = new JwtToken();
        }

        [Fact]
        public void JwtToken_CanBeInitialized()
        {
            var token = new JwtToken();
            Assert.NotNull(token);
        }

        [Fact]
        public void JwtToken_SetToken_StoresToken()
        {
            _token.Token = "test";
            Assert.Equal("test", _token.Token);
        }

        [Fact]
        public void JwtToken_SetMessage_StoresMessage()
        {
            _token.Message = "test";
            Assert.Equal("test", _token.Message);
        }
    }
}