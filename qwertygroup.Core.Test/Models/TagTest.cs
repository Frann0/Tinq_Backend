using qwertygroup.Core.Models;
using Xunit;

namespace qwertygroup.Core.Test.Models
{
    public class TagTest
    {
        private Tag _tag;
        public TagTest(){
            _tag = new Tag();
        }

        [Fact]
        public void Tag_CanBeInitialized()
        {
            var tag = new Tag();
            Assert.NotNull(tag);
        }

        [Fact]
        public void Tag_Id_MustBeInt()
        {
            Assert.True(_tag.Id is int);
        }

        [Fact]
        public void Tag_SetId_StoresId()
        {
            var tag = new Tag();
            tag.Id = 1;
            Assert.Equal(1, tag.Id);
        }

        [Fact]
        public void Tag_SetName_StoresNameAsString()
        {
            var tag = new Tag();
            tag.Text = "Tag";
            Assert.Equal("Tag", tag.Text);
        }
    }
}