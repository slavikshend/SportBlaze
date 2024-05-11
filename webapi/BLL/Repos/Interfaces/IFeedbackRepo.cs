using webapi.DAL.Entities.MN;

namespace webapi.BLL.Repos.Interfaces
{
    public interface IFeedbackRepo
    {
        Task<IEnumerable<FeedBack>> GetAllFeedbacksAsync();
        Task<IEnumerable<FeedBack>> GetAllProductFeedbacksAsync(int productId);
        Task<FeedBack> AddFeedbackAsync(FeedBack feedback);
        Task<bool> DeleteFeedbackAsync(int feedbackId);
    }
}
