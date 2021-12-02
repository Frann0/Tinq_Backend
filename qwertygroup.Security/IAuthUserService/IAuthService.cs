using System;
using Microsoft.AspNetCore.Identity;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public interface IAuthService
    {
        JwtToken GenerateJwtToken(string username, string password);
        string HashedPassword(string password, byte[] salt);
        byte[] CreateSalt();
        AuthUser FindUser(string loginDtoUsername);
        AuthUser CreateUser(IdentityUser identityUser, string registerDtoPassword);
    }
}