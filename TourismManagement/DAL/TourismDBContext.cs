using Microsoft.EntityFrameworkCore;
using TourismManagement.Models;

namespace TourismManagement.DAL
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
        public DbSet<Payment> Payments { get; set; }
    }




    }
