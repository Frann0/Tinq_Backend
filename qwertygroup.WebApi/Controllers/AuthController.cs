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
using qwertygroup.Security.Models;
using qwertygroup.WebApi.Dtos;

namespace qwertygroup.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
       
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login([FromBody] LoginDto loginDto)
        {
            var authUser = _authService.FindUser(loginDto.Username);
            

            if (authUser == null)
            {
                return BadRequest("Login failed");
            }

            var token = _authService.GenerateJwtToken(loginDto.Username, loginDto.Password);
            return new TokenDto()
            {
                Token = token.Token,
                Message = "Success!"
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
            
            var user = _authService.CreateUser(identityUser, registerDto.Password);

            if (user != null)
            {
                return BadRequest("User Registration Failed");
            }

            return Ok("User Registration Successful");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Oink()
        {
            return Ok("Oink!");
        }
    }
}