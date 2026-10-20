using AboutMeWeb.Enums;

namespace AboutMeWeb.Models
{
    public class ServiceCatalogItemViewModel
    {
        public ServiceType ServiceType { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public string Icon { get; set; } = string.Empty;
    }
}