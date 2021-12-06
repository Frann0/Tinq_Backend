using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Core.IServices;
using qwertygroup.Core.Models;
using qwertygroup.WebApi.Dtos;

namespace qwertygroup.WebApi.Controllers
{
    [Route("[controller]")]
    public class PostController : Controller
    {

        //TODO finish testing for this class, i was too lazy
        private readonly IPostService _postService;
        private readonly IBodyService _bodyService;
        private readonly ITitleService _titleService;

        public PostController(IPostService postService, IBodyService bodyService, ITitleService titleService)
        {
            _postService = postService;
            _bodyService = bodyService;
            _titleService = titleService;
        }

        [HttpGet("/posts")]
        public ActionResult<IEnumerable<PostDto>> GetAllPosts()
        {
            return Ok(_postService.GetAllPosts().Select(
                post => new PostDto(post)));
        }

        [HttpGet("{id}")]
        public ActionResult<PostDto> GetPost(int id)
        {
            Post post = _postService.GetPost(id);
            return Ok(new PostDto(post));
        }

        [HttpPost]
        public ActionResult<PostDto> CreatePost([FromQuery]String titleText, String bodyText, int userId)
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
            return Ok(new PostDto(post));
        }

//TODO SHOULD BE AUTHORIZED
        [HttpPatch]
        public ActionResult<PostDto> UpdatePost([FromQuery]String titleText, String bodyText, int postId)
        {
            Post post = _postService.GetPost(postId);
            _titleService.UpdateTitle(new Title{Id=post.TitleId,Text=titleText});
            _bodyService.UpdateBody(new Body{Id=post.BodyId,Text=bodyText});
            return Ok(new PostDto(_postService.GetPost(postId)));
        }

//TODO SHOULD BE AUTHORIZED
        [HttpDelete("/del/{postId}")]
        public ActionResult DeletePost(int postId){
            try{
            Post post = _postService.GetPost(postId);
            _titleService.DeleteTitle(post.TitleId);
            _bodyService.DeleteBody(post.BodyId);
            _postService.DeletePost(post);
            return Ok("Post deleted.");
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}