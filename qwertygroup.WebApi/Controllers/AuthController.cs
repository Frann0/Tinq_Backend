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
using qwertygroup.WebApi.PolicyHandlers;

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

        
        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null) // add Modelstate ?
            {
                return BadRequest("User Registration Failed");
            }

            var authUser = new AuthUser()
            {
                Username = registerDto.Username,
                Email = registerDto.Email
            };
            
            var user = _authService.CreateUser(authUser, registerDto.Password);

            if (user != null)
            {
                return BadRequest("User Registration Failed");
            }

            return Ok("User Registration Successful");
        }

        [Authorize(nameof(AdminUserHandler))]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            return Ok("Auth works!");
        }
        
        // TODO DeleteUser
        [Authorize(nameof(RegisteredUserHandler))]
        [HttpDelete]
        public ActionResult DeleteUser([FromBody] UserDto userDto)
        {
            var user = _authService.FindUser(userDto.Username);
            return user != null ? Ok(_authService.DeleteUser(user)) : BadRequest("Delete user failed");
        }
        
        // TODO EditPermissions
        [Authorize(nameof(AdminUserHandler))]
        [HttpPut]
        public ActionResult<UserDto> EditUserPermissions()
        {
            return null;
        }

        // TODO BanUser
        
        
    }
}