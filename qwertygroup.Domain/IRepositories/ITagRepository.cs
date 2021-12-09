using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.IRepositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAllTags();
        Tag CreateTag(Tag tag);
        void RemoveTagRelations(int tagId);
    }
}