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
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task CreateAsync(Service service)
        {
            _context.Services.Add(service);

            await _context.SaveChangesAsync();
        }
    }
}
