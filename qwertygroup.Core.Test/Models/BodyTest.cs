using qwertygroup.Core.Models;
using Xunit;

namespace qwertygroup.Core.Test.Models
{
    public class BodyTest
    {
        
        private Body _body;
        public BodyTest(){
            _body = new Body();
        }

        [Fact]
        public void Body_CanBeInitialized()
        {
            var body = new Body();
            Assert.NotNull(body);
        }

        [Fact]
        public void Body_Id_MustBeInt()
        {
            Assert.True(_body.Id is int);
        }

        [Fact]
        public void Body_SetId_StoresId()
        {
            var body = new Body();
            body.Id = 1;
            Assert.Equal(1, body.Id);
        }

        [Fact]
        public void Body_SetName_StoresNameAsString()
        {
            var body = new Body();
            body.Text = "Body";
            Assert.Equal("Body", body.Text);
        }
    }
}