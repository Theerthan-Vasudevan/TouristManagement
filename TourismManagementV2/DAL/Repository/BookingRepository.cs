using Microsoft.EntityFrameworkCore;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;

namespace TourismManagementV2.DAL.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TourismDBContext context;
        public BookingRepository(TourismDBContext context)
        {
            this.context = context;
        }
        public void AddBooking(Booking booking)
        {
            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            var booking = context.Bookings.Find(id);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                context.SaveChanges();
                
            }
            else
            {
                throw new Exception("Booking not found");
            }
        }

        public void UpdateBooking(Booking booking)
        {
            context.Bookings.Update(booking);
            context.SaveChanges();
        }

        public Booking GetBookingById(int id)
        {
            return context.Bookings
                          .Include(b => b.Package) // include related package
                          .FirstOrDefault(b => b.BookingId == id);
        }

        public List<Booking> GetAllBookings()
        {
            return context.Bookings
                           .Include(b => b.User)     // Include User
                           .Include(b => b.Package)  // Include Package
                           .ToList();
        }

        public List<Booking> GetBookingsByPackage(int packageId)
        {
            List<Booking> bookings = context.Bookings.Where(b => b.PackageId == packageId).ToList();
            return bookings;
        }

        public List<Booking> GetBookingsByUser(int userId)
        {
            List<Booking> bookings = context.Bookings.Where(b => b.UserId == userId).ToList();
            return bookings;
        }


    }
}
