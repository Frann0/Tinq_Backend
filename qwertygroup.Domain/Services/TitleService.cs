using System;
using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.Domain.Services
{
    public class TitleService : ITitleService
    {
        private readonly ITitleRepository _titleRepository;

        public TitleService(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository ?? throw new System.MissingFieldException(
                "TitleService Must have a TitleRepository!");
        }


        public List<Title> GetTitles()
        {
            return _titleRepository.GetTitles().ToList();
        }

        public Title GetTitle(int id)
        {
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new System.InvalidOperationException(message);
            }
            else
            {
                try
                {
                    return _titleRepository.GetTitles().First(t => t.Id == id);
                }
                catch (Exception e)
                {
                    throw new System.InvalidOperationException($"No title with given id: {id}");
                }
            }
        }

        public Title CreateTitle(string text)
        {
            return _titleRepository.CreateTitle(text);
        }

        public void DeleteTitle(int id)
        {
            if (id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new System.InvalidOperationException(message);
            }
            else
            {
                try
                {
                    _titleRepository.DeleteTitle(id);

                }
                catch (Exception)
                {
                    throw new System.InvalidOperationException($"No title with given id: {id}");
                }
            }
        }

        public Title UpdateTitle(Title title)
        {
            if (title.Id <= 0)
            {
                string message = "Id must be greater than 0!";
                throw new System.InvalidOperationException(message);
            }
            try
            {

                return _titleRepository.UpdateTitle(title);

            }
            catch (System.Exception)
            {
                throw new System.InvalidOperationException($"No body with given id {title.Id}");
            }
        }

    }
}