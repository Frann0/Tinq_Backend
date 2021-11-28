using System.Linq;
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
            var authUserEntity = _authDbContext.LoginUsers.FirstOrDefault(user =>
                user.Username.Equals(username) && user.Password.Equals(password));
            
            if (authUserEntity == null) return null;

            return new AuthUser()
            {
                Id = authUserEntity.Id,
                Username = authUserEntity.Username
            };
        }
    }
}