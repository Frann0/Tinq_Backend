using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using qwertygroup.Core.IServices;
using qwertygroup.WebApi.Dtos;

namespace qwertygroup.WebApi.Controllers
{
    [Route("[controller]")]
    public class BodyController : Controller
    {
        private readonly IBodyService _bodyService;

        public BodyController(IBodyService bodyService){
            _bodyService=bodyService;
        }

        [HttpGet]
        public IEnumerable<BodyDto> getAllPosts(){
            return _bodyService.GetBodies().Select(
                newBody=>new BodyDto{Text=newBody.Text}
                ).ToList();
        }

    }
}