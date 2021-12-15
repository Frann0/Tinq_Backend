using System.Collections.Generic;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.IRepositories
{
    public interface IAuthUserRepository
    {
        // AuthUser FindByUsernameAndPassword(string username, string password);
        AuthUser FindUser(string email);
        List<Permission> GetUserPermissions(int id);
        bool DeleteUser(AuthUser user);

        bool CreateUser(AuthUser newUser);
        IEnumerable<AuthUser> GetAllUsersWithPermissions();

        List<AuthUser> GetAllUsers();
        bool AssignAdminPermissionToUser(AuthUser user);
        bool RemoveAdminPermissionFromUser(AuthUser user);
       
    }
}