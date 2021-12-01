using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.IRepositories
{
    public interface ITitleRepository
    {
        IEnumerable<Title> GetTitles();
        Title CreateTitle(string text);
        void DeleteTitle(int id);
        Title UpdateTitle(Title newTitle);
    }
}