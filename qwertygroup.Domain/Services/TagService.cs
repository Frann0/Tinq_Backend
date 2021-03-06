using System.Collections.Generic;
using System;
using System.Linq;
using qwertygroup.Core.Models;
using qwertygroup.Core.IServices;
using qwertygroup.Domain.IRepositories;

namespace qwertygroup.Domain.Services
{
    public class TagService : MyIdentifyable,ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;

        public TagService(ITagRepository tagRepository, IPostTagRepository postTagRepository)
        {
            _tagRepository = tagRepository??throw new MissingFieldException("TagService Must have a TagRepository!");
            _postTagRepository = postTagRepository??throw new MissingFieldException("TagService Must have a PostTagRepository!");
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepository.GetAllTags().ToList();
        }

        public Tag CreateTag(Tag newTag)
        {
            return _tagRepository.CreateTag(newTag);
        }
        
        public Tag CreateTag(int postId,Tag newTag)
        {
            Tag t = _tagRepository.CreateTag(newTag);
            _postTagRepository.CreatePostTagRelationship(newTag.Id,postId);
            return t;
        }

        public Tag UpdateTag(Tag newTag)
        {
            CheckId(newTag.Id);
            return _tagRepository.UpdateTag(newTag);
        }

        public void DeleteTag(int tagId)
        {
            CheckId(tagId);
            _tagRepository.DeleteTag(tagId);
        }
    }
}