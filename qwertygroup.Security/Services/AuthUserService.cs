using qwertygroup.Security.IRepositories;
using qwertygroup.Security.Models;

namespace qwertygroup.Security
{
    public class AuthUserService : IAuthUserService.IAuthUserService
    {
        private readonly IAuthUserRepository _authUserRepository;

        public AuthUserService(IAuthUserRepository authUserRepository)
        {
            _authUserRepository = authUserRepository;
        }
        
        public AuthUser GetUser(string username)
        {
            return _authUserRepository.FindUser(username);
        }

        
    }
}