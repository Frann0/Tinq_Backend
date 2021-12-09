using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using System;

namespace qwertygroup.Domain.Services
{
    public class BodyService : MyIdentifyable,IBodyService
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
            CheckId(id);
            try
            {
                return _bodyRepository.GetBodies().First(b => b.Id == id);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"No Body with given id: {id}");
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
            CheckId(id);
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
            CheckId(body.Id);
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