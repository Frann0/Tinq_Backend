using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.WebApi.Dtos;
using qwertygroup.WebApi.PolicyHandlers;

namespace qwertygroup.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        private readonly IBodyService _bodyService;
        private readonly ITitleService _titleService;
        private readonly ITagService _tagService;

        public PostController(IPostService postService, IBodyService bodyService, ITitleService titleService, ITagService tagService)
        {
            _postService = postService;
            _bodyService = bodyService;
            _titleService = titleService;
            _tagService = tagService;
        }

        [AllowAnonymous]
        [HttpGet("/posts")]
        public ActionResult<IEnumerable<PostDto>> GetAllPosts()
        {
            return Ok(_postService.GetAllPosts().Select(
                post => new PostDto(post)));
        }

        [AllowAnonymous]
        [HttpGet("/posts/{userId}")]
        public ActionResult<IEnumerable<PostDto>> GetPostsByUserID(int userId)
        {
            return Ok(_postService.GetPostByUserID(userId).Select(
                post => new PostDto(post)));
        }

        [AllowAnonymous]
        [HttpGet("/search/{query}")]
        public ActionResult<IEnumerable<PostDto>> GetPostsByUserID(string query)
        {
            return Ok(_postService.GetPostsBySearchString(query).Select(
                post => new PostDto(post)));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<PostDto> GetPost(int id)
        {
            Post post = _postService.GetPost(id);
            return Ok(new PostDto(post));
        }

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpPost("/{postId}/{tag}")]
        public ActionResult<Post> AddPostTagRelation(int postId, string tag)
        {
            Tag tagWithId = _tagService.CreateTag(new Tag { Text = tag });
            _postService.CreatePostTagRelation(postId, tagWithId.Id);
            return Ok();
        }

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpPost]
        public ActionResult<PostDto> CreatePost([FromQuery] String titleText, String bodyText, int userId, String tags)
        {
            Body body = _bodyService.CreateBody(bodyText);
            Title title = _titleService.CreateTitle(titleText);
            Post post = _postService.CreatePost(new Post
            {
                TitleId = title.Id,
                Title = titleText,
                BodyId = body.Id,
                Body = bodyText,
                UserId = userId
            });
            List<Tag> tagList = tags.Split(",").Select(t => _tagService.CreateTag(post.Id, new Tag { Text = t })).ToList();
            post.Tags = tagList;
            return Ok(new PostDto(post));
        }

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpPatch]
        public ActionResult<PostDto> UpdatePost([FromQuery] String titleText, String bodyText, int postId)
        {
            Post post = _postService.GetPost(postId);
            _titleService.UpdateTitle(new Title { Id = post.TitleId, Text = titleText });
            _bodyService.UpdateBody(new Body { Id = post.BodyId, Text = bodyText });
            return Ok(new PostDto(_postService.GetPost(postId)));
        }

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpDelete("/del/{postId}/{userId}")]
        public ActionResult DeletePost(int postId, int userId)
        {
            try
            {
                Post post = _postService.GetPost(postId);
                if (post.UserId != userId)
                    return BadRequest("You must be owner of a post to delete it!");
                _titleService.DeleteTitle(post.TitleId);
                _bodyService.DeleteBody(post.BodyId);
                _postService.DeletePost(post);
                return Ok("Post deleted.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpDelete("/del/{postId}")]
        public ActionResult DeletePost(int postId)
        {
            try
            {
                Post post = _postService.GetPost(postId);
                _titleService.DeleteTitle(post.TitleId);
                _bodyService.DeleteBody(post.BodyId);
                _postService.DeletePost(post);
                return Ok("Post deleted.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}