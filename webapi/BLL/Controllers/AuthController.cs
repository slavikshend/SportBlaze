using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NuGet.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Services.Implementations;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.Main;

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
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdateUserAsync(model, userEmail);
            if (!result)
            {
                return NotFound("User not found");
            }

            return Ok(new { message = "User information updated successfully" });
        }

        [Authorize(Roles = "RegisteredUser,Admin")]
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var userInfo = await _service.GetAsync(userEmail);
            if (userInfo == null)
            {
                return NotFound("User not found");
            }

            return Ok(userInfo);
        }

        [HttpPost("changePassword")]
        [Authorize(Roles = "RegisteredUser,Admin")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.ChangePasswordAsync(userEmail, model.OldPassword, model.NewPassword);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(new { message = "Password changed successfully" });
        }
    }
}
