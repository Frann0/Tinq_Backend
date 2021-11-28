using System;
using System.Collections.Generic;
using System.Linq;
using qwertygroup.Core.Models;
using qwertygroup.DataAccess.Entities;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.DataAccess.Repositories
{
    public class TitleRepository : ITitleRepository
    {
        private readonly PostContext _context;

        public TitleRepository(PostContext context)
        {
            _context = context;
        }

        public List<Title> GetTitles()
        {
            return _context.titles.Select(title => new Title { Id = title.Id, Text = title.Text }).ToList();
        }

        public Title CreateTitle(string text)
        {
            TitleEntity titleEntity = new TitleEntity { Text = text };
            _context.titles.Add(titleEntity);
            _context.SaveChanges();
            return new Title { Id = titleEntity.Id, Text = titleEntity.Text };
        }

        public void DeleteTitle(int id)
        {
            try
            {
                TitleEntity titleEntity = _context.titles.First(t => t.Id == id);
                _context.titles.Remove(titleEntity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Title UpdateTitle(Title newTitle)
        {
            try
            {
                TitleEntity titleEntity = _context.titles.First(t => t.Id == newTitle.Id);
                titleEntity.Text = newTitle.Text;
                _context.SaveChanges();
                return new Title { Id = titleEntity.Id, Text = titleEntity.Text };
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}