using webapi.BLL.Models;
using webapi.DAL.Entities.MN;

namespace webapi.BLL.Services.Interfaces
{
    public interface IFeedBackService
    {
        Task<IEnumerable<FeedBackModel>> GetAllProductFeedbacksAsync(int productId);
        Task<FeedBackModel> AddFeedback(int productId, string userEmail, FeedBackModel feedbackModel);
        Task<bool> DeleteFeedback(int feedbackId);
    }
}
