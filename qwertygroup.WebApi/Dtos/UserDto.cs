using System.Collections.Generic;
using System.Security;
using qwertygroup.Security.Models;

namespace qwertygroup.WebApi.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }
        public TokenDto Token { get; set; }
    }
}