using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nito.AsyncEx;
using qwertygroup.Security;
using qwertygroup.WebApi.Dtos;

namespace qwertygroup.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly UserManager<IdentityUser> _userManager;


        public AuthController(ISecurityService securityService, UserManager<IdentityUser> userManager)
        {
            _securityService = securityService;
            _userManager = userManager;
        }
       
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login(LoginDto dto)
        {
            var token = _securityService.GenerateJwtToken(dto.Username, dto.Password);
            return new TokenDto()
            {
                Token = token.Token,
                Message = token.Message
            };
        }

        [HttpPost(nameof(Register))]
        public ActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
            {
                return BadRequest("User Registration Failed");
            }

            var identityUser = new IdentityUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            
            var result = _userManager.CreateAsync(identityUser, registerDto.Password).Result;

            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (var error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new{Message = "User Registration Failed", Errors = dictionary});
            }

            return Ok("User Registration Successful");
        }
        
        
    }

    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}