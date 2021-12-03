using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Core.IServices
{
    public interface IPostService
    {
        List<Post> GetAllPosts();
        Post CreatePost(Post post);
        Post GetPost(int id);
        void DeletePost(Post post);
    }
}