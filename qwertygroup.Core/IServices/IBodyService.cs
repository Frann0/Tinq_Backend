using System.Collections.Generic;
using qwertygroup.Core.Models;

namespace qwertygroup.Core.IServices
{
    public interface IBodyService
    {
        List<Body> GetBodies();
        Body CreateBody(string text);
        void DeleteBody(int id);
        Body UpdateBody(Body body);
    }
}