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
using qwertygroup.WebApi.Helpers;
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
        public ActionResult<UserDto> Login([FromBody] LoginDto loginDto)
        {
            // validate
            var result =  InputValidator(loginDto);
            
            var authUser = _authService.FindUser(loginDto.Email);
            

            if (authUser == null)
            {
                return BadRequest("Login failed");
            }

            var token = _authService.GenerateJwtToken(authUser, loginDto.Password);
            return new UserDto()
            {
                Username = authUser.Username,
                Email = authUser.Email,
                Permissions = authUser.Permissions,
                Token = new TokenDto()
                {
                    Token = token.Token,
                    Message = "Success!"
                }
            };
        }

        
        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid && registerDto == null) // add Modelstate ?
            {
                return BadRequest("User Registration Failed");
            }
            
            // check email exist

            var authUser = new AuthUser()
            {
                Username = UsernameGenerator.GenerateRandomUsername(),
                Email = registerDto.Email
            };
            
            var user = _authService.CreateUser(authUser, registerDto.Password);

            return user ? Ok("Registration Succeeded!") : BadRequest("Registration Failed");
        }

        [Authorize(nameof(AdminUserHandler))]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var users = _authService.GetAllUsers();
            return null;
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

        private bool InputValidator<T>(T input)
        {
            return ModelState.IsValid;
        }
    }
}