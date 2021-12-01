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
        private readonly IPostService _postService;
        private readonly IBodyService _bodyService;
        private readonly ITitleService _titleService;

        public PostController(IPostService postService,IBodyService bodyService, ITitleService titleService){
            _postService = postService;
            _bodyService = bodyService;
            _titleService = titleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> GetAllPosts(){
            return Ok(_postService.GetAllPosts().Select(
                p=>new PostDto{
                    Body=p.Body,
                    Title=p.Title,
                    UserId=p.UserId
                }));
        }
    }
}