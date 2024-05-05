using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using webapi.BLL.Services.Interfaces;

namespace webapi.BLL.Controllers
{
    [Authorize(Roles = "RegisteredUser,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesService _favouritesService;

        public FavouritesController(IFavouritesService favouritesService)
        {
            _favouritesService = favouritesService;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToFavourites(int productId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found in claims.");
            }
            await _favouritesService.AddToFavourites(productId, userEmail);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteFromFavourites(int productId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found in claims.");
            }

            await _favouritesService.DeleteFromFavourites(productId, userEmail);
            return Ok();
        }
    }
}
