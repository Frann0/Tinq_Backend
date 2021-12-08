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
        public ActionResult<bool> Register([FromBody] RegisterDto registerDto)
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

            return user;
        }

        
        [HttpGet("allusers")]
        public ActionResult<List<UserListDto>> GetAllUsers()
        {
            var users = _authService.GetAllUsers().Select(u => new UserListDto()
            {
                Username = u.Username,
                Email = u.Email,
                Id = u.Id
            }).ToList();
            return users;
        }
        
        

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpDelete("deleteprofile")]
        public ActionResult DeleteProfile([FromBody] UserDto userDto)
        {
            var user = _authService.FindUser(userDto.Username);
            return user != null ? Ok(_authService.DeleteUser(user)) : BadRequest("Delete user failed");
        }
        
        [Authorize(nameof(AdminUserHandler))]
        [HttpDelete("deleteuser")]
        public ActionResult<bool> AdminDeleteUser([FromBody] UserDto userDto)
        {
            var user = _authService.FindUser(userDto.Username);
            return user != null ? Ok(_authService.DeleteUser(user)) : BadRequest("Delete user failed");
        }
        
        [Authorize(nameof(AdminUserHandler))]
        [HttpPut()]
        public ActionResult<bool> UpdateUserPermissions()
        {
            return null;
        }


        private bool InputValidator<T>(T input)
        {
            return ModelState.IsValid;
        }
    }
}