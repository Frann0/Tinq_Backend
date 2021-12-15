using System.Collections.Generic;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.IServices
{
    public interface IAuthService
    {
        AuthUser FindUser(string email);
        bool CreateUser(AuthUser identityUser, string registerDtoPassword);
        bool DeleteUser(AuthUser user);
        bool AdminDeleteUser(AuthUser user);
        List<Permission> GetPermissions(int id);
        List<AuthUser> GetAllUsers();
        bool AssignAdminPermissionToUser(AuthUser user);
        bool RemoveAdminPermissionFromUser(AuthUser user);
        JwtToken GenerateJwtToken(AuthUser user, string password);
        bool Authenticate(AuthUser user, string password);
        string HashedPassword(string password, byte[] userSalt);
        byte[] CreateSalt();

    }
}