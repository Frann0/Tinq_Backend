using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Core.IServices
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        Tag CreateTag(Tag newTag);
        Tag CreateTag(int postId,Tag newTag);
    }
}