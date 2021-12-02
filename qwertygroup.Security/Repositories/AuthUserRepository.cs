using System.Linq;
using System.Text;
using qwertygroup.Security.IRepositories;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly AuthDbContext _authDbContext;

        public AuthUserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public AuthUser FindUser(string username)
        {
            // TODO include permissions
            
            var authUserEntity = _authDbContext.AuthUsers.FirstOrDefault(user =>
                user.Username.Equals(username));
            
            if (authUserEntity == null) return null;

            return new AuthUser()
            {
                Id = authUserEntity.Id,
                Username = authUserEntity.Username,
                HashedPassword = authUserEntity.HashedPassword,
                Salt = authUserEntity.Salt
            };
        }
        
        // TODO CreateUser
        
        // TODO UpdateUser
        
        // TODO AddPermission
        
        // TODO RemovePermission
    }
}