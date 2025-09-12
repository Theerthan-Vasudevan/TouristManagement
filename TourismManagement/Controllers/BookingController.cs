using Microsoft.AspNetCore.Mvc;
using TourismManagement.Models;
using TourismManagement.NewFolder;

namespace TourismManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService bookingService;
        private readonly PackageService packageService;

        // Hardcoded test user ID
        private readonly int testUserId = 2;

        public BookingController(BookingService bookingService, PackageService packageService)
        {
            this.bookingService = bookingService;
            this.packageService = packageService;
        }

        // Show all bookings for the fake user
        public IActionResult MyBookings()
        {
            var bookings = bookingService.GetUserBookings(testUserId);
            return View(bookings);
        }

        // Booking form
        [HttpGet]
        public IActionResult Create(int packageId)
        {
            var package = packageService.GetPackageById(packageId);
            ViewBag.PackageName = package.PackageName;
            ViewBag.PackagePrice = package.Price;
            ViewBag.PackageId = package.PackageId;

            return View(new Booking { PackageId = package.PackageId, NumberOfPeople = 1 });
        }

        // Submit booking
        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            booking.UserId = testUserId; // use fake user
            var package = packageService.GetPackageById(booking.PackageId);

            booking.TotalAmount = booking.NumberOfPeople * package.Price;

            bookingService.CreateBooking(booking);

            return RedirectToAction("MyBookings");
        }

        // Cancel booking
        public IActionResult Cancel(int id)
        {
            bookingService.CancelBooking(id);
            return RedirectToAction("MyBookings");
        }
    }
}
