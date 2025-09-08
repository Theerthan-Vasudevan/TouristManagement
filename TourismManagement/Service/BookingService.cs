using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.NewFolder
{
    public class BookingService
    {
        private readonly IBookingRepository bookingRepo;
        private readonly IPackageRepository packageRepo;

        public BookingService(IBookingRepository bookingRepo, IPackageRepository packageRepo)
        {
            this.bookingRepo = bookingRepo;
            this.packageRepo = packageRepo;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return bookingRepo.GetAll();
        }

        public Booking GetBookingById(int id)
        {
            return bookingRepo.GetById(id);
        }

        public IEnumerable<Booking> GetUserBookings(int userId)
        {
            return bookingRepo.GetByUserId(userId);
        }

        public void CreateBooking(Booking booking)
        {
            var pkg = packageRepo.GetById(booking.PackageId);
            booking.BookingDate = DateTime.Now;
            booking.TotalAmount = booking.NumberOfPeople * pkg.Price;
            booking.Status = "Confirmed";

            bookingRepo.Add(booking);
            bookingRepo.Save();
        }

        public void CancelBooking(int bookingId)
        {
            var booking = bookingRepo.GetById(bookingId);

            // Apply cancellation rule: refund 85%
            decimal refund = booking.TotalAmount * 0.85m;
            booking.Status = "Cancelled";
            booking.TotalAmount = refund;

            bookingRepo.Update(booking);
            bookingRepo.Save();
        }

        public object GenerateReport()
        {
            var bookings = bookingRepo.GetAll();
            var totalRevenue = 0m;
            foreach (var b in bookings)
                totalRevenue += b.TotalAmount;

            return new
            {
                TotalBookings = bookings.Count(),
                TotalRevenue = totalRevenue
            };
        }
    }
}
