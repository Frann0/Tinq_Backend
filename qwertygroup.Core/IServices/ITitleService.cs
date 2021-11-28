using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Core.IServices
{
    public interface ITitleService
    {
        List<Title> GetTitles();
        Title CreateTitle(string text);
        void DeleteTitle(int id);
        Title UpdateTitle(Title title);
    }
}