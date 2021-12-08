using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public interface IAuthService
    {
        JwtToken GenerateJwtToken(AuthUser user, string password);
        string HashedPassword(string password, byte[] salt);
        byte[] CreateSalt();
        AuthUser FindUser(string email);
        bool CreateUser(AuthUser identityUser, string registerDtoPassword);
        List<Permission> GetPermissions(int id);
        bool DeleteUser(AuthUser user);
        bool AdminDeleteUser(AuthUser user);
        List<AuthUser> GetAllUsers();
        AuthUser AssignAdminPermissionToUser(AuthUser user);
        AuthUser RemoveAdminPermissionFromUser(AuthUser user);

    }
}