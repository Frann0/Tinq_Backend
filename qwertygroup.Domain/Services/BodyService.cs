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
            _bodyRepository = bodyRepository ?? throw new System.MissingFieldException(
                "BodyService Must have a BodyRepository!");
        }

        public Body CreateBody(string text)
        {
            if (text.Length <= 3)
                throw new System.InvalidOperationException("A Post's body must be longer than three characters!");
            return _bodyRepository.CreateBody(text);
        }

        public void DeleteBody(int id)
        {
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new System.InvalidOperationException(message);
            }
            try
            {
                _bodyRepository.DeleteBody(id);
            }
            catch (System.Exception e)
            {
                throw new System.InvalidOperationException($"No body with given id {id}");
            }
        }

        public List<Body> GetBodies()
        {
            return _bodyRepository.GetBodies();
        }

        public Body UpdateBody(Body body)
        {
            if (body.Id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new System.InvalidOperationException(message);
            }
            try
            {
                return _bodyRepository.UpdateBody(body);
            }
            catch (System.Exception e)
            {
                throw new System.InvalidOperationException($"No body with given id {body.Id}");
            }
        }
    }
}