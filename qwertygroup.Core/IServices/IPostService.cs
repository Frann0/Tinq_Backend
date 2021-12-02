using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Core.IServices
{
    public interface IPostService
    {
        List<Post> GetAllPosts();
    }
}