using System.Collections.Generic;

namespace qwertygroup.Security.Models
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public byte[] Salt { get; set; }
        public List<Permission> Type { get; set; }
        public int DbUserId { get; set; }
        public string Email { get; set; }
    }
}