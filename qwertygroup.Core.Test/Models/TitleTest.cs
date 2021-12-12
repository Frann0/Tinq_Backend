using qwertygroup.Core.Models;
using Xunit;

namespace qwertygroup.Core.Test.Models
{
    public class TitleTest
    {
        private Title _title;
        public TitleTest()
        {
            _title = new Title();
        }

        [Fact]
        public void Title_CanBeInitialized()
        {
            var title = new Title();
            Assert.NotNull(title);
        }

        [Fact]
        public void Title_Id_MustBeInt()
        {
            Assert.True(_title.Id is int);
        }

        [Fact]
        public void Title_SetId_StoresId()
        {
            var title = new Title();
            title.Id = 1;
            Assert.Equal(1, title.Id);
        }

        [Fact]
        public void Title_SetName_StoresNameAsString()
        {
            var title = new Title();
            title.Text = "Title";
            Assert.Equal("Title", title.Text);
        }
    }
}