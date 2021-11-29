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
        
        
        public AuthUser FindByUsernameAndPassword(string username, string password)
        {
            return null;
        }

        public AuthUser FindUser(string username)
        {
            /*
            var authUserEntity = _authDbContext.AuthUsers.FirstOrDefault(user =>
                user.Username.Equals(username));
            
            if (authUserEntity == null) return null;

            return new AuthUser()
            {
                Id = authUserEntity.Id,
                Username = authUserEntity.Username,
                HashedPassword = authUserEntity.HashedPassword,
                Salt = Encoding.ASCII.GetBytes(authUserEntity.Salt)
            };
            */
            return null;
        }
    }
}