using System.ComponentModel.DataAnnotations;

namespace Salon.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Staff ID")]
        public string StaffId { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
