using System;
using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.Domain.Services
{
    public class TitleService : MyIdentifyable, ITitleService
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
            CheckId(id);
            try
            {
                return _titleRepository.GetTitles().First(t => t.Id == id);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"No Title with given id: {id}");
            }
        }

        public Title CreateTitle(string text)
        {
            return _titleRepository.CreateTitle(text);
        }

        public void DeleteTitle(int id)
        {
            CheckId(id);
            try
            {
                _titleRepository.DeleteTitle(id);

            }
            catch (Exception)
            {
                throw new InvalidOperationException($"No Title with given id: {id}");
            }
        }

        public Title UpdateTitle(Title title)
        {
            CheckId(title.Id);
            try
            {
                return _titleRepository.UpdateTitle(title);

            }
            catch (System.Exception)
            {
                throw new InvalidOperationException($"No Title with given id: {title.Id}");
            }

        }
    }
}