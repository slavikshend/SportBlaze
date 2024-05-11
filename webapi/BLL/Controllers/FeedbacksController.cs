using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.BLL.Models;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.MN;

namespace webapi.BLL.Controllers
{
    [Authorize(Roles = "RegisteredUser,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedBackService _feedbackService;

        public FeedbacksController(IFeedBackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAllProductFeedbacks(int productId)
        {
            var feedbacks = await _feedbackService.GetAllProductFeedbacksAsync(productId);
            return Ok(feedbacks);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddFeedback(int productId, FeedBackModel feedbackModel)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("User email not found in claims.");
            }

            var addedFeedback = await _feedbackService.AddFeedback(productId, userEmail, feedbackModel);
            return Ok(addedFeedback);
        }

        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback( int feedbackId)
        {
            var deletedFeedback = await _feedbackService.DeleteFeedback(feedbackId);
            if (deletedFeedback == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
