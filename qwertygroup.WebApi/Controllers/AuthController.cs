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
                return BadRequest("Authentication failed");
            }
            
            var authUser = _authService.FindUser(loginDto.Email);

            if (authUser == null)
            {
                return Unauthorized("Authentication failed");
            }

            if (_authService.Authenticate(authUser, loginDto.Password))
            {
                var token = _authService.GenerateJwtToken(authUser);
                

                return new UserDto()
                {
                    Id = authUser.Id,
                    Username = authUser.Username,
                    Email = authUser.Email,
                    Permissions = authUser.Permissions,
                    Token = token.Token
                };
            }
            else
            {
                return Unauthorized("Authentication failed");
            }
        }

        [HttpGet(nameof(GetCurrentUser))]
        [Authorize]
        public ActionResult<UserDto> GetCurrentUser()
        {

            var httpContextUser = HttpContext.Items["LoginUser"] as AuthUser;
            AuthUser authUser = null;
            if (httpContextUser != null)
            {
                authUser = _authService.FindUser(httpContextUser.Email);
            }
            

            if (authUser == null)
            {
                return NotFound("Current user not found!");
            }
            
            return new UserDto()
            {
                Id = authUser.Id,
                Username = authUser.Username,
                Email = authUser.Email,
                Permissions = authUser.Permissions,
                Token = _authService.GenerateJwtToken(authUser).Token
            };

        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult<bool> Register([FromBody] RegisterDto registerDto)
        {
            if (IsInvalidInput(registerDto)) 
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
        
        

        [Authorize(nameof(AdminUserHandler))]
        [HttpDelete("deleteprofile")]
        public ActionResult DeleteProfile([FromBody]UserDto userDto)
        {
            if (IsInvalidInput(userDto))
            {
                return BadRequest("Invalid user information");
            }
            var user = _authService.FindUser(userDto.Email);
            
            return user != null ? Ok(_authService.DeleteUser(user)) : BadRequest("Delete user failed");
        }

        [Authorize(nameof(AdminUserHandler))]
        [HttpPost("assignadmin/{email}")]
        public ActionResult<bool> AssignAdminUserPermission(string email)
        {
            if (IsInvalidInput(email))
            {
                return BadRequest("Invalid user information");
            }
            
            var user = _authService.FindUser(email);

            if (user == null)
            {
                return NotFound("User not found");
            }
            
            return _authService.AssignAdminPermissionToUser(user);
        }

        [Authorize(nameof(AdminUserHandler))]
        [HttpDelete("removeadmin")]
        public ActionResult<bool> RemoveAdminUserPermission(UserDto userDto)
        {
            if (IsInvalidInput(userDto))
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

        [Authorize(nameof(AdminUserHandler))]
        [HttpGet(nameof(GetAllUsersWithPermissions))]
        public ActionResult<List<UserListDto>> GetAllUsersWithPermissions()
        {
            return _authService.GetAllUsersWithPermissions().Select(u => new UserListDto()
            {
                Username = u.Username,
                Email = u.Email,
                Permissions = u.Permissions
            }).ToList();
        }

        private bool IsInvalidInput<T>(T input)
        {
            return !ModelState.IsValid || input == null;
        }
    }
}