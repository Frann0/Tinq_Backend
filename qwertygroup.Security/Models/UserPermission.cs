using System.Reflection.Metadata.Ecma335;

namespace qwertygroup.Security.Models
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public AuthUser User { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}