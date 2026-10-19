using AboutMeWeb.Common;
using AboutMeWeb.Enums;
using System.ComponentModel.DataAnnotations;

namespace AboutMeWeb.Models
{
    public class CreateServiceViewModel
    {
        [EnumDataType(typeof(ServiceType))]
        [Display(Name = "Service")]
        public ServiceType ServiceType { get; set; } = ServiceType.GmodServer;

        [EnumDataType(typeof(ServiceTier))]
        [Display(Name = "Tier")]
        public ServiceTier Tier { get; set; } = ServiceTier.Basic;

        [Required]
        [StringLength(ValidationConstants.ServiceDescriptionMaxLength, MinimumLength = ValidationConstants.ServiceDescriptionMinLength)]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;
    }
}