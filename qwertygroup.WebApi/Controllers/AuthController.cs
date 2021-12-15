using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nito.AsyncEx;
using qwertygroup.Security;
using qwertygroup.Security.IServices;
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
        private readonly Permission ADMIN_PERMISSION;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            ADMIN_PERMISSION = new Permission() {Id = 2, Name = "Admin"};
        }
       
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult<UserDto> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid || loginDto.Password == null)
            {
                return BadRequest("User Registration Failed");
            }
            
            var authUser = _authService.FindUser(loginDto.Email);

            if (authUser == null)
            {
                return Unauthorized("No user account associated with email");
            }

            var token = _authService.GenerateJwtToken(authUser, loginDto.Password);

            if (token.Token == null)
            {
                return Unauthorized(token);
            }
            
            return new UserDto()
            {
                Id = authUser.Id,
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

        [HttpGet(nameof(GetCurrentUser))]
        [Authorize]
        public ActionResult<UserDto> GetCurrentUser()
        {

            var authUser = HttpContext.Items["LoginUser"] as AuthUser;

            if (authUser == null)
            {
                return NotFound("Current user not found!");
            }

            // Define whats needed to return
            return new UserDto()
            {
                Id = authUser.Id,
                Username = authUser.Username,
                Email = authUser.Email,
                Permissions = authUser.Permissions
            };

        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult<bool> Register([FromBody] RegisterDto registerDto)
        {
            if (InputValidator(registerDto)) 
            {
                return BadRequest("User Registration Failed");
            }

            var userQuery = _authService.FindUser(registerDto.Email);
            
            if (userQuery != null)
            {
                return BadRequest("Email already registered to another user.");
            }
            
            var authUser = new AuthUser()
            {
                Username = UsernameGenerator.GenerateRandomUsername(),
                Email = registerDto.Email
            };
            
            var user = _authService.CreateUser(authUser, registerDto.Password);

            return user;
        }

        [Authorize(nameof(AdminUserHandler))]
        [HttpGet("allusers")]
        public ActionResult<List<UserListDto>> GetAllUsers()
        {
            var users = _authService.GetAllUsers().Select(u => new UserListDto()
            {
                Username = u.Username,
                Email = u.Email,
                Id = u.Id
            }).ToList();

            if (!users.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            return users;
        }
        
        

        [Authorize(nameof(RegisteredUserHandler))]
        [HttpDelete("deleteprofile")]
        public ActionResult DeleteProfile([FromBody] UserDto userDto)
        {
            if (InputValidator(userDto))
            {
                return BadRequest("Invalid user information");
            }
            var user = _authService.FindUser(userDto.Username);
            
            return user != null ? Ok(_authService.DeleteUser(user)) : BadRequest("Delete user failed");
        }
        
        [Authorize(nameof(AdminUserHandler))]
        [HttpDelete("deleteuser")]
        public ActionResult<bool> AdminDeleteUser([FromBody] UserDto userDto)
        {
            if (InputValidator(userDto))
            {
                return BadRequest("Invalid user information");
            }
            var user = _authService.FindUser(userDto.Username);
            
            return user != null ? Ok(_authService.DeleteUser(user)) : BadRequest("Delete user failed");
        }
        
        [Authorize(nameof(AdminUserHandler))]
        [HttpPost()]
        public ActionResult<bool> AssignAdminUserPermission(UserDto userDto)
        {
            if (InputValidator(userDto))
            {
                return BadRequest("Invalid user information");
            }
            
            var user = _authService.FindUser(userDto.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }
            
            return _authService.AssignAdminPermissionToUser(user);
        }

        [Authorize(nameof(AdminUserHandler))]
        [HttpDelete()]
        public ActionResult<bool> RemoveAdminUserPermission(UserDto userDto)
        {
            if (InputValidator(userDto))
            {
                return BadRequest("Invalid user information");
            }
            
            var user = _authService.FindUser(userDto.Email);
            
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            return _authService.RemoveAdminPermissionFromUser(user);
        }

        private bool InputValidator<T>(T input)
        {
            return !ModelState.IsValid || input == null;
        }
    }
}