using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagementV2.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }   // Primary Key

        [ForeignKey("User")]
        public int UserId { get; set; }      // FK → User
        [ForeignKey("Package")]
        public int PackageId { get; set; }   // FK → Package

        public DateTime BookingDate { get; set; }
        public string? Status { get; set; }   // "CONFIRMED", "CANCELLED", "PENDING"
                                              // New fields
        public int NumberOfPeople { get; set; }
        public double TotalAmount { get; set; }
        // Navigation properties (useful if you’re using EF Core)
        public User? User { get; set; }
        public Package? Package { get; set; }
    }
}
