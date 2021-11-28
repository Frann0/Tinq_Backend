using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.Services
{
    public interface IBodyRepository
    {
        List<Body> GetBodies();
    }
}