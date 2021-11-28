using qwertygroup.Security.Models;

namespace qwertygroup.Security.IAuthUserService
{
    public interface IAuthUserService
    {
        AuthUser GetUser(string username);
    }
}