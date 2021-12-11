using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using System;
using qwertygroup.Domain.IRepositories;

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
        
        public List<Body> GetBodies()
        {
            return _bodyRepository.GetBodies().ToList();
        }
        public Body GetBody(int id)
        {
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new InvalidOperationException(message);
            }
            else
            {
                try
                {
                    return _bodyRepository.GetBodies().First(b => b.Id == id);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"No title with given id: {id}");
                }
            }
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
                throw new InvalidOperationException(message);
            }
            try
            {
                _bodyRepository.DeleteBody(id);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"No body with given id: {id}");
            }
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
            catch (System.Exception)
            {
                throw new System.InvalidOperationException($"No body with given id {body.Id}");
            }
        }

        
    }
}