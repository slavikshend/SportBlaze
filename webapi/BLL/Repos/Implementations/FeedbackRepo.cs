using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Context;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.MN;

namespace webapi.BLL.Repos.Implementations
{
    public class FeedbackRepo : IFeedbackRepo
    {
        private readonly SportsShopDbContext _context;

        public FeedbackRepo(SportsShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedBack>> GetAllFeedbacksAsync()
        {
            return await _context.FeedBacks
                .Include(f => f.User)
                .Include(f => f.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<FeedBack>> GetAllProductFeedbacksAsync(int productId)
        {
            return await _context.FeedBacks
                .Include(f => f.User)
                .Include(f => f.Product)
                .Where(f => f.Product.Id == productId)
                .ToListAsync();
        }

        public async Task<FeedBack> AddFeedbackAsync(FeedBack feedback)
        {
            try
            {
                _context.FeedBacks.Add(feedback);
                await _context.SaveChangesAsync();

                feedback = await _context.FeedBacks
                    .Include(f => f.Product)
                    .Include(f => f.User)
                    .FirstOrDefaultAsync(f => f.Id == feedback.Id);

                return feedback;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _context.FeedBacks.FindAsync(feedbackId);
            if (feedback == null)
                return false;

            _context.FeedBacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
