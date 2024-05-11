using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Interfaces;
using webapi.DAL.Entities.MN;

namespace webapi.BLL.Services.Implementations
{
    public class FeedbackService : IFeedBackService
    {
        private readonly IFeedbackRepo _feedbackRepo;

        public FeedbackService(IFeedbackRepo feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }

        public async Task<IEnumerable<FeedBackModel>> GetAllProductFeedbacksAsync(int productId)
        {
            var feedbacks = await _feedbackRepo.GetAllProductFeedbacksAsync(productId);
            var models = new List<FeedBackModel>();
            foreach (var feedback in feedbacks)
            {
                var model = new FeedBackModel
                {
                    Email = feedback.UserEmail,
                    Rating = feedback.Rating,
                    Date = feedback.Date,
                    Comment = feedback.Comment
                };
                models.Add(model);
            }
            return models;
        }

        public async Task<FeedBackModel> AddFeedback(int productId, string userEmail, FeedBackModel feedbackModel)
        {
            var feedback = new FeedBack
            {
                UserEmail = userEmail,
                Rating = feedbackModel.Rating,
                Date = feedbackModel.Date,
                Comment = feedbackModel.Comment,
                ProductId = productId 
            };
            var addedFeedback = await _feedbackRepo.AddFeedbackAsync(feedback);

            feedbackModel = new FeedBackModel
            {
                Email = addedFeedback.UserEmail,
                Rating = addedFeedback.Rating,
                Date = addedFeedback.Date,
                Comment = addedFeedback.Comment
            };
            return feedbackModel;
        }

        public async Task<bool> DeleteFeedback(int feedbackId)
        {
            var result = await _feedbackRepo.DeleteFeedbackAsync(feedbackId);
            return result;
        }
    }
}
