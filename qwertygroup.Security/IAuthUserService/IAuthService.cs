using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public interface IAuthService
    {
        JwtToken GenerateJwtToken(string username, string password);
        string HashedPassword(string password, byte[] salt);
        byte[] CreateSalt();
        AuthUser FindUser(string loginDtoUsername);
        bool CreateUser(AuthUser identityUser, string registerDtoPassword);
        List<Permission> GetPermissions(int id);
        bool DeleteUser(AuthUser user);
        ActionResult<List<AuthUser>> GetAllUsers();
    }
}