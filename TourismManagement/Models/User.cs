using System.ComponentModel.DataAnnotations;

namespace TourismManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(255)]
        public string PasswordHash { get; set; }  // Store hashed password

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "Customer"; // "Admin" or "Customer"

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
