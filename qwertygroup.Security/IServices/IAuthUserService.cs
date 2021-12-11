using System.Collections.Generic;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.IServices
{
    public interface IAuthUserService
    {
        AuthUser FindUser(string email);
        bool CreateUser(AuthUser user, string password);
        bool DeleteUser(AuthUser user);
        bool AdminDeleteUser(AuthUser user);
        List<AuthUser> GetAllUsers();
        List<Permission> GetPermissions(int id);

    }
}