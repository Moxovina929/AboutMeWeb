using AboutMeWeb.Data;
using AboutMeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AboutMeWeb.Services
{
    public class ServiceCatalogService : IServiceCatalogService
    {
        private readonly ApplicationDbContext _context;
        public ServiceCatalogService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services
                .Include(s => s.User)
                .Include(s => s.Review)
                .AsNoTracking()
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }
        public async Task<List<Service>> GetUserOrdersAsync(string userId)
        {
            return await _context.Services
                .Include(s => s.Review)
                .Where(s => s.UserId == userId)
                .AsNoTracking()
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }
        public async Task<Service?> GetUserOrderAsync(int id, string userId)
        {
            return await _context.Services
                .Include(s => s.Review)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }
        public async Task CreateAsync(Service service)
        {
            _context.Services.Add(service);

            await _context.SaveChangesAsync();
        }
    }
}
