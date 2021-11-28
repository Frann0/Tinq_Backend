using qwertygroup.Security.Models;

namespace qwertygroup.Security.IRepositories
{
    public interface IAuthUserRepository
    {
        AuthUser FindByUsernameAndPassword(string username, string password);
    }
}