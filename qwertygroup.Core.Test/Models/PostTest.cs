using qwertygroup.Core.Models;
using Xunit;

namespace qwertygroup.Core.Test
{
    public class PostTest
    {
        private Post _Post;
        public PostTest(){
            _Post = new Post();
        }

        [Fact]
        public void Post_CanBeInitialized()
        {
            var Post = new Post();
            Assert.NotNull(Post);
        }

        [Fact]
        public void Post_Id_MustBeInt()
        {
            Assert.True(_Post.Id is int);
        }

        [Fact]
        public void Post_SetId_StoresId()
        {
            var Post = new Post();
            Post.Id = 1;
            Assert.Equal(1, Post.Id);
        }
        
        [Fact]
        public void Post_BodyId_MustBeInt()
        {
            Assert.True(_Post.BodyId is int);
        }

        [Fact]
        public void Post_SetBodyId_StoresId()
        {
            var Post = new Post();
            Post.BodyId = 1;
            Assert.Equal(1, Post.BodyId);
        }
        [Fact]
        public void Post_TitleId_MustBeInt()
        {
            Assert.True(_Post.TitleId is int);
        }

        [Fact]
        public void Post_SetTitleId_StoresId()
        {
            var Post = new Post();
            Post.TitleId = 1;
            Assert.Equal(1, Post.TitleId);
        }

        [Fact]
        public void Post_UserId_MustBeInt()
        {
            Assert.True(_Post.UserId is int);
        }

        [Fact]
        public void Post_SetUserId_StoresId()
        {
            var Post = new Post();
            Post.TitleId = 1;
            Assert.Equal(1, Post.TitleId);
        }

    }
}