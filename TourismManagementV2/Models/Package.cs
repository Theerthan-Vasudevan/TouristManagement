using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismManagementV2.Models
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int DurationDays { get; set; }
        public string? ImageUrl { get; set; }
        public List<Booking>? Bookings { get; set; }
    }
}
