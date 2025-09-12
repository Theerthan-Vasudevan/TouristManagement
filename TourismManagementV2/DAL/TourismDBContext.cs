using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TourismManagementV2.Models;

namespace TourismManagementV2.DAL
{
    public class TourismDBContext : DbContext
    {
        public TourismDBContext()
        {
        }
        public TourismDBContext(DbContextOptions<TourismDBContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
