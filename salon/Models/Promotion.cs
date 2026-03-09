using System.ComponentModel.DataAnnotations;

namespace Salon.Models   // ← change to your project namespace if needed
{
    public class Promotion
    {
        public int Id { get; set; }   // Primary key

        [Required]
        [StringLength(100)]
        [Display(Name = "Promotion Title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Details")]
        public string? Description { get; set; }
    }
}
