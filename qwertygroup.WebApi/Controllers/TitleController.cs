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
    public class TitleController : Controller
    {
        private readonly ITitleService _titleService;

        public TitleController(ITitleService titleService)
        {
            _titleService = titleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TitleDto>> getAllTitles()
        {
            return Ok(_titleService.GetTitles().Select(
                newTitle => new TitleDto { Text = newTitle.Text }
            ).ToList());
        }

        [HttpPost]
        public ActionResult<Title> CreateTitle([FromBody] TitleDto titleDto)
        {
            try
            {
                return Ok(_titleService.CreateTitle(titleDto.Text));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteTitle(int id)
        {
            try
            {
                _titleService.DeleteTitle(id);
                return Ok($"Deleted {id}.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public ActionResult<Title> UpdateTitle([FromBody] Title title)
        {
            try
            {
                return Ok(_titleService.UpdateTitle(title));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}