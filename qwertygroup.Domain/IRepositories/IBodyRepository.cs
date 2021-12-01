using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Domain.Services
{
    public interface IBodyRepository
    {
        IEnumerable<Body> GetBodies();
        Body CreateBody(string text);
        void DeleteBody(int id);
        Body UpdateBody(Body body);
    }
}