using System.ComponentModel.DataAnnotations;

namespace AboutMeWeb.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(80, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 10000)]
        public decimal Price { get; set; }
    }
}
