using AboutMeWeb.Common;
using System.ComponentModel.DataAnnotations;
using AboutMeWeb.Enums;

namespace AboutMeWeb.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.ServiceTitleMaxLength,
            MinimumLength = ValidationConstants.ServiceTitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(
            ValidationConstants.ServiceDescriptionMaxLength,
            MinimumLength = ValidationConstants.ServiceDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(
            ValidationConstants.ServicePriceMinValue,
            ValidationConstants.ServicePriceMaxValue)]
        public decimal Price { get; set; }
        public ServiceType ServiceType { get; set; }

        public ServiceTier Tier { get; set; }
    }
}
