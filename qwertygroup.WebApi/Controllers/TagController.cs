using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;

namespace qwertygroup.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Tag> GetAllTags()
        {
            return _tagService.GetAllTags();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<Tag> CreateTag(string text)
        {
            return Ok(_tagService.CreateTag(new Tag { Text = text }));
        }

        [AllowAnonymous]
        [HttpPost("{postId}/{text}")]
        public ActionResult<Tag> CreateTag(int postId, string text)
        {
            Tag newTag = new Tag { Text = text };
            return Ok(_tagService.CreateTag(postId, newTag));

        }

        [HttpPatch("{tagId}/u/{text}")]
        public ActionResult<Tag> UpdateTag(int tagId, string text)
        {
            Tag newTag = new Tag { Id = tagId, Text = text };
            return Ok(_tagService.UpdateTag(newTag));


        }

        [HttpDelete("d/{tagId}")]
        public ActionResult<Tag> DeleteTag(int tagId)
        {
            _tagService.DeleteTag(tagId);
            return Ok();


        }
    }
}