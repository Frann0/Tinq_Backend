using System.Collections.Generic;
using qwertygroup.Security.Models;

namespace qwertygroup.WebApi.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}