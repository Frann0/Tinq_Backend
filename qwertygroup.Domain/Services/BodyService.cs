using System.Collections.Generic;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.Services
{
    public class BodyService : IBodyService
    {
        private IBodyRepository _bodyRepository;

        public BodyService(IBodyRepository bodyRepository)
        {
            _bodyRepository = bodyRepository??throw new System.MissingFieldException(
                "BodyRepository must not be null!");
        }

        public List<Body> GetBodies()
        {
            return _bodyRepository.GetBodies();
        }
    }
}