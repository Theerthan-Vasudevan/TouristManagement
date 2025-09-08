using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagement.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, StringLength(50)]
        public string PaymentMethod { get; set; } // e.g., Credit Card, UPI, NetBanking

        [StringLength(50)]
        public string Status { get; set; } = "Paid"; // Paid, Failed, Refunded

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
