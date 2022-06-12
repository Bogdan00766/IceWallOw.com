using Domain.Models;
using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace IceWallOw.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<User> _logger;

        public UsersController(IUserService userService, ILogger<User> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public IActionResult Post(RegisterUserDto rud)
        {
            _logger.LogInformation("Register POST request");
            UserDto newUser;
            try
            {
                newUser = _userService.Register(rud.Name, rud.LastName, rud.Password, rud.EMail);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            _logger.LogCritical($"User {newUser.Name} created");
            return Created("/api/users/login", newUser);
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> PostAsync(LoginUserDto lud)
        {
            
            UserDto user;
            try
            {
                user = _userService.Login(lud.EMail, lud.Password);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Wrong password")) return BadRequest("Wrong password");
                return StatusCode(500);
            }

            var resp = new HttpResponseMessage();

            Guid guid = Guid.NewGuid();
            _userService.SetGuid(guid, user.Id);
            HttpContext.Response.Cookies.Append("GUID",guid.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
            });

            return Ok(user);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            var guidString = Request.Cookies["GUID"];
            Guid guid;
            try
            {
                guid = Guid.Parse(guidString);
            }
            catch(Exception e)
            {
                _logger.LogCritical($"Error while parsing guid: {guidString}");
                return BadRequest("Error while parsing guid");
            }
            try
            {
                _userService.Logout(guid);
                return Ok("User logged out sucessfully");
            }
            catch(Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpDelete]
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

    }
}
