using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService userService)
        {
            _service = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _service.LoginAsync(model);
            if (token == null)
            {
                return Unauthorized(new LoginResult { Successful = false, Error = "Invalid email or password" });
            }
            return Ok(new LoginResult { Successful = true, Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.RegisterAsync(model);
            if (!result)
            {
                return BadRequest(new { message = "User registration failed" });
            }

            return Ok(new { message = "User registered successfully" });
        }

        [Authorize(Roles = "RegisteredUser,Admin")]
        [HttpPut("edituser")]
        public async Task<IActionResult> EditUser(UserModel model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateUserAsync(model, userEmail);
            if (!result)
            {
                return NotFound("User not found");
            }

            return Ok("User information updated successfully");
        }
    }
}
