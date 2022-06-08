using IceWallOw.Application.Dto;
using IceWallOw.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IceWallOw.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Post(RegisterUserDto rud)
        {
            UserDto newUser;
            try
            {
                newUser = _userService.Register(rud.Name, rud.LastName, rud.Password, rud.EMail);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created("/api/users/login", newUser);
        }

        [HttpPost("Login")]
        public IActionResult Post(LoginUserDto lud)
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
            return Ok(user);
        }
    }
}
