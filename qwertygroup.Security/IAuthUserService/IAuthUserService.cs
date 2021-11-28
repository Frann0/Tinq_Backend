using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public interface IAuthUserService
    {
        AuthUser Login(string username, string password);
    }
}