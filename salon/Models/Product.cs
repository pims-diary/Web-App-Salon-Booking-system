using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salon.Models  // ← change namespace if your project is named differently
{
    public class Product
    {
        public int Id { get; set; }   // Primary key

        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        //used for searching by date
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
    }
}
