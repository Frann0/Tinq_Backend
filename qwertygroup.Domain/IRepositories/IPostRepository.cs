using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.IRepositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
    }
}