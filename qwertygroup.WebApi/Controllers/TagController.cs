using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;

namespace qwertygroup.WebApi.Controllers
{
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IEnumerable<Tag> GetAllTags()
        {
            return _tagService.GetAllTags();
        }

        [HttpPost]
        public ActionResult<Tag> CreateTag(string text)
        {
            return Ok(_tagService.CreateTag(new Tag { Text = text }));
        }

        [HttpPost("{postId}/"+"{text}")]
        public ActionResult<Tag> CreateTag(int postId, string text)
        {
            Tag newTag = new Tag{Text=text};
            return Ok(_tagService.CreateTag(postId,newTag));
            
        }
    }
}