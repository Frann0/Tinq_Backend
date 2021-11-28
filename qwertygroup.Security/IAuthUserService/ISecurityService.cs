using System;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string username, string password);
        string HashedPassword(string password, byte[] salt);
    }
}