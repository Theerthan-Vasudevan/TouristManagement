using TourismManagementV2.Models;

namespace TourismManagementV2.DAL.Interface
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();           // List all bookings
        Booking GetBookingById(int id);                  // Get booking by primary key
        void AddBooking(Booking booking);                // Create a new booking
        void UpdateBooking(Booking booking);             // Edit an existing booking
        void DeleteBooking(int id);                      // Cancel/Delete a booking

        // Optional, but useful
        List<Booking> GetBookingsByUser(int userId);     // User’s booking history
        List<Booking> GetBookingsByPackage(int packageId); // Who booked a package
    }
}
