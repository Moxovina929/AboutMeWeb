using AboutMeWeb.Models;

namespace AboutMeWeb.Services
{
    public interface IServiceCatalogService
    {
        Task<List<Service>> GetAllAsync();
        Task<List<Service>> GetUserOrdersAsync(string userId);
        Task<Service?> GetUserOrderAsync(int id, string userId);
        Task CreateAsync(Service service);
    }
}
