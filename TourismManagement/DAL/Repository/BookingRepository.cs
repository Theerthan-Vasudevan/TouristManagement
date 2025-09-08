using Microsoft.EntityFrameworkCore;
using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.DAL.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TourismDBContext context;

        public BookingRepository(TourismDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Booking> GetAll() =>
            context.Bookings.Include("User").Include("Package").ToList();

        public Booking GetById(int id)
        {
            var booking = context.Bookings
                                 .Include("User")
                                 .Include("Package")
                                 .FirstOrDefault(b => b.BookingId == id);
            if (booking == null) throw new Exception($"Booking with ID {id} not found.");
            return booking;
        }

        public IEnumerable<Booking> GetByUserId(int userId)
        {
            var bookings = context.Bookings.Where(b => b.UserId == userId).ToList();
            if (!bookings.Any()) throw new Exception($"No bookings found for User ID {userId}.");
            return bookings;
        }

        public void Add(Booking booking)
        {
            context.Bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
           context.Bookings.Update(booking);
        }

        public void Delete(int id)
        {
            var booking = context.Bookings.Find(id);
            if (booking == null) throw new Exception($"Booking with ID {id} not found for deletion.");
            context.Bookings.Remove(booking);
        }

        public void Save() => context.SaveChanges();
    }
}
