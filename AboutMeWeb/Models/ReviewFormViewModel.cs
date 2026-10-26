using AboutMeWeb.Common;
using System.ComponentModel.DataAnnotations;

namespace AboutMeWeb.Models
{
    public class ReviewFormViewModel
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string ServiceTitle { get; set; } = string.Empty;

        [Range(
            ValidationConstants.ReviewRatingMinValue,
            ValidationConstants.ReviewRatingMaxValue,
            ErrorMessage = "Choose from 1 to 5 stars.")]
        [Display(Name = "Rating")]
        public int Rating { get; set; } = 5;

        [Required]
        [StringLength(
            ValidationConstants.ReviewCommentMaxLength,
            MinimumLength = ValidationConstants.ReviewCommentMinLength)]
        [Display(Name = "Your review")]
        public string Comment { get; set; } = string.Empty;
    }
}
