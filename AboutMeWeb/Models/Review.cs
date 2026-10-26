using AboutMeWeb.Common;
using System.ComponentModel.DataAnnotations;

namespace AboutMeWeb.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(
            ValidationConstants.ReviewRatingMinValue,
            ValidationConstants.ReviewRatingMaxValue)]
        public int Rating { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.ReviewCommentMaxLength,
            MinimumLength = ValidationConstants.ReviewCommentMinLength)]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; } = null!;
    }
}
