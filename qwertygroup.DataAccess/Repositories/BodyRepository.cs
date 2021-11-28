using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.Models;
using qwertygroup.Domain.Services;

namespace qwertygroup.DataAccess.Repositories
{
    public class BodyRepository : IBodyRepository
    {
        private readonly PostContext _context;

        public BodyRepository(PostContext context)
        {
            _context = context;
        }
        public List<Body> GetBodies()
        {
            return _context.bodies.Select(
                b => new Body { Id = b.Id, Text = b.Text }).ToList();
        }
    }
}