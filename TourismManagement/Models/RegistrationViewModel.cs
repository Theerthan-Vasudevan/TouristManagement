using System.ComponentModel.DataAnnotations;

namespace TourismManagement.Models
{
    public class RegistrationViewModel
    {
        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }
    }
}