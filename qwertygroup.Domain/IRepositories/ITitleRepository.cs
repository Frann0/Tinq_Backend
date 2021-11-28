using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.IRepositories
{
    public interface ITitleRepository
    {
        List<Title> GetTitles();
        Title CreateTitle(string text);
        void DeleteTitle(int id);
        Title UpdateTitle(Title title2);
    }
}