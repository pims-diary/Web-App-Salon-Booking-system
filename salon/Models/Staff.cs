using System.ComponentModel.DataAnnotations;

namespace Salon.Models
{
    public class Staff
    {
        public int Id { get; set; }   // Primary key

        [Required]
        [Display(Name = "Staff ID")]
        public string StaffId { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
