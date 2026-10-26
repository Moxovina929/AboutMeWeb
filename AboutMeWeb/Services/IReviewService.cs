using AboutMeWeb.Models;

namespace AboutMeWeb.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetUserReviewAsync(int id, string userId);
        Task CreateAsync(Review review);
        Task UpdateAsync(int id, int rating, string comment);
        Task DeleteAsync(int id);
    }
}
