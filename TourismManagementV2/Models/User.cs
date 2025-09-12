using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagementV2.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }

        public string? Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Role { get; set; }      // "Admin" or "User"
        public List<Booking>? Bookings { get; set; }

    }
}
