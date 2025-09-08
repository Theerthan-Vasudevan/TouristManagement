using System.ComponentModel.DataAnnotations;

namespace TourismManagement.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required, StringLength(200)]
        public string PackageName { get; set; }

        [Required, StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; }

        public int DurationDays { get; set; }

        [StringLength(200)]
        public string ImageUrl { get; set; }   // Store path of package image

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
