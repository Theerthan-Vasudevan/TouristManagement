using Microsoft.AspNetCore.Mvc;
using TourismManagement.Models;
using TourismManagement.NewFolder;

namespace TourismManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService bookingService;
        private readonly PackageService packageService;

        public BookingController(BookingService bookingService, PackageService packageService)
        {
            this.bookingService = bookingService;
            this.packageService = packageService;
        }

        // List bookings of current user
        public ActionResult MyBookings()
        {
            // Replace this line:
            // int userId = Convert.ToInt32(Session["UserId"]);
            // With the following:
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var bookings = bookingService.GetUserBookings(userId);
            return View(bookings);
        }

        // Create booking
        [HttpGet]
        public ActionResult Create(int packageId)
        {
            var package = packageService.GetPackageById(packageId);
            ViewBag.PackageName = package.PackageName;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Booking booking)
        {
            booking.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            booking.TotalAmount = booking.NumberOfPeople * booking.Package.Price;

            bookingService.CreateBooking(booking);
            return RedirectToAction("MyBookings");
        }

        // Cancel booking
        public ActionResult Cancel(int id)
        {
            bookingService.CancelBooking(id); // you can implement 15% deduction logic in service
            return RedirectToAction("MyBookings");
        }
    }
}
