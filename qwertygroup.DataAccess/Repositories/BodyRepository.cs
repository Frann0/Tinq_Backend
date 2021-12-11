using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.Models;
using qwertygroup.DataAccess.Entities;
using qwertygroup.Domain.IRepositories;
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

        public Body CreateBody(string text)
        {
            BodyEntity bodyEntity = new BodyEntity { Text = text };
            _context.bodies.Add(bodyEntity);
            _context.SaveChanges();
            return new Body { Id = bodyEntity.Id, Text = bodyEntity.Text };
        }

        public void DeleteBody(int id)
        {
            try
            {
                BodyEntity bodyEntity = _context.bodies.First(e => e.Id == id);
                _context.bodies.Remove(bodyEntity);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IEnumerable<Body> GetBodies()
        {
            return _context.bodies.Select(
                body => new Body { Id = body.Id, Text = body.Text }
            );
        }

        public Body UpdateBody(Body body)
        {
            try
            {
                BodyEntity bodyEntity = _context.bodies.First(e => e.Id == body.Id);
                bodyEntity.Text = body.Text;
                _context.SaveChanges();
                return new Body { Id = bodyEntity.Id, Text = bodyEntity.Text };
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}