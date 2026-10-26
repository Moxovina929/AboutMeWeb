using AboutMeWeb.Data;
using AboutMeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AboutMeWeb.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Service)
                .ThenInclude(s => s.User)
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }
        public async Task<Review?> GetUserReviewAsync(int id, string userId)
        {
            return await _context.Reviews
                .Include(r => r.Service)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id && r.Service.UserId == userId);
        }
        public async Task CreateAsync(Review review)
        {
            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, int rating, string comment)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return;
            }

            review.Rating = rating;
            review.Comment = comment;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return;
            }

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();
        }
    }
}
