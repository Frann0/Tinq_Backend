using System.Collections.Generic;
using qwertygroup.Security.Models;

namespace qwertygroup.Security.IServices
{
    public interface IAuthService
    {
        JwtToken GenerateJwtToken(AuthUser user, string password);
        string HashedPassword(string password, byte[] salt);
        byte[] CreateSalt();
        bool Authenticate(AuthUser dbUser, string plainTextPassword);
        AuthUser FindUser(string email);
        bool CreateUser(AuthUser identityUser, string registerDtoPassword);
        List<Permission> GetPermissions(int id);
        bool DeleteUser(AuthUser user);
        bool AdminDeleteUser(AuthUser user);
        List<AuthUser> GetAllUsers();
        bool AssignAdminPermissionToUser(AuthUser user);
        bool RemoveAdminPermissionFromUser(AuthUser user);

    }
}