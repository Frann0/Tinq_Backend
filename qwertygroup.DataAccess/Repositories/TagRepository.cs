using System.Collections.Generic;
using System.Linq;
using qwertygroup.DataAccess.Entities;
using System;
using qwertygroup.Core.Models;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.DataAccess.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly PostContext _context;

        public TagRepository(PostContext context)
        {
            _context = context;
        }



        public IEnumerable<Tag> GetAllTags()
        {
            return _context.tags.Select(t => new Tag { Id = t.Id, Text = t.Text });
        }

        public Tag CreateTag(Tag tag)
        {
            try
            {
                TagEntity existingTag = _context.tags.First(t => tag.Text.ToUpper() == t.Text.ToUpper());
                tag.Id = existingTag.Id;
                return tag;
            }
            catch (Exception)
            {
                TagEntity tagEntity = new TagEntity { Text = tag.Text };
                _context.tags.Add(tagEntity);
                _context.SaveChanges();
                tag.Id = tagEntity.Id;
                return tag;
            }
        }

        public void DeleteTag(int tagId)
        {
            TagEntity tag = _context.tags.First(t => t.Id == tagId);
            _context.tags.Remove(tag);
            _context.SaveChanges();
            RemoveTagRelations(tagId);
        }
        public void RemoveTagRelations(int tagId)
        {
            var query = _context.postTags.Where(pt => pt.tagId == tagId);
            foreach (var item in query.ToList())
            {
                _context.postTags.Remove(item);
            }
            _context.SaveChanges();
        }

        public Tag UpdateTag(Tag newTag)
        {
            TagEntity tag = _context.tags.First(t => t.Id == newTag.Id);
            tag.Text=newTag.Text;
            _context.SaveChanges();
            return tag.ToTag();
        }
    }
}