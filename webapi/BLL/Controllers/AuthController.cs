using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _service = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            // Validate user input  

            var token = GenerateJwtToken(model.Email);

            return Ok(new { token });
        }

        [HttpPost("signup")]
        public IActionResult Register(RegisterModel model)
        {
            // Validate user input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create user in the database (you need to implement this logic)
            // Return appropriate response based on the result
            // For demo purpose, let's assume user creation is successful
            return Ok("User created successfully");
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
