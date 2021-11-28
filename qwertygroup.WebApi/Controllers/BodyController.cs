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
    public class BodyController : Controller
    {
        private readonly IBodyService _bodyService;

        public BodyController(IBodyService bodyService)
        {
            _bodyService = bodyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BodyDto>> getAllPosts()
        {
            return Ok(_bodyService.GetBodies().Select(
                newBody => new BodyDto { Text = newBody.Text }
                ).ToList());
        }

        [HttpPost]
        public ActionResult<Body> CreateBody([FromBody] BodyDto bodyDto)
        {
            try
            {
                return Ok(_bodyService.CreateBody(bodyDto.Text));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteBody(int id)
        {
            try
            {
                _bodyService.DeleteBody(id);
                return Ok($"Deleted {id}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public ActionResult<Body> UpdateBody([FromBody] Body body)
        {
            try
            {
                return Ok(_bodyService.UpdateBody(body));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}