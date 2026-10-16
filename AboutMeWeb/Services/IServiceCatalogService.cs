using AboutMeWeb.Models;

namespace AboutMeWeb.Services
{
    public interface IServiceCatalogService
    {
        Task<List<Service>> GetAllAsync();
        Task CreateAsync(Service service);
    }
}
