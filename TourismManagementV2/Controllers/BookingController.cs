using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;
using System;
using System.Linq;

namespace TourismManagementV2.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IPackageRepository _packageRepo;

        public BookingController(IBookingRepository bookingRepo, IPackageRepository packageRepo)
        {
            _bookingRepo = bookingRepo;
            _packageRepo = packageRepo;
        }

        // ===============================
        // USER FLOW
        // ===============================

        // List bookings of logged-in user
        public IActionResult UserIndex()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            var bookings = _bookingRepo.GetAllBookings()
                                       .Where(b => b.UserId == userId.Value)
                                       .ToList();
            return View(bookings);
        }

        // Step 1: Show booking form (user selects number of people)
        [HttpGet]
        public IActionResult Book(int id)
        {
            var package = _packageRepo.GetById(id);
            if (package == null) return NotFound();

            var booking = new Booking
            {
                PackageId = package.PackageId,
                Package = package
            };

            return View(booking);
        }


        // Step 2: Handle booking form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(Booking booking)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            var package = _packageRepo.GetById(booking.PackageId);
            if (package == null) return NotFound();

            booking.UserId = userId.Value;
            booking.BookingDate = DateTime.Now;
            booking.Status = "PENDING PAYMENT";
            booking.TotalAmount = package.Price * booking.NumberOfPeople;

            if (!ModelState.IsValid)
            {
                booking.Package = package;
                return View(booking);
            }

            _bookingRepo.AddBooking(booking);

            return RedirectToAction("Payment", new { bookingId = booking.BookingId });
        }

        // Step 3: Payment Page
        public IActionResult Payment(int bookingId)
        {
            var booking = _bookingRepo.GetBookingById(bookingId);
            if (booking == null) return NotFound();

            if (booking.Package == null)
                booking.Package = _packageRepo.GetById(booking.PackageId);

            return View(booking); // Payment.cshtml
        }

        // Step 4: Confirm Payment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmPayment(int bookingId, string paymentMethod)
        {
            var booking = _bookingRepo.GetBookingById(bookingId);
            if (booking == null) return NotFound();

            booking.Status = "PAID";
            _bookingRepo.UpdateBooking(booking);

            ViewBag.PaymentMethod = paymentMethod;
            return View("PaymentSuccess", booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();

            // ✅ Only allow the user who booked it to cancel
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || booking.UserId != userId.Value)
                return Unauthorized();

            // ✅ Refund calculation (85%)
            double refundAmount = booking.TotalAmount * 0.85;

            // Update status
            booking.Status = "CANCELLED";
            _bookingRepo.UpdateBooking(booking);

            // Pass refund amount to view
            ViewBag.RefundAmount = refundAmount;

            return View("CancelConfirmation", booking);
        }


        // ===============================
        // ADMIN FLOW
        // ===============================

        // List all bookings
        public IActionResult AdminIndex()
        {
            var bookings = _bookingRepo.GetAllBookings();
            return View(bookings);
        }

        // Booking details
        public IActionResult Details(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        // Edit booking
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookingRepo.UpdateBooking(booking);
                return RedirectToAction("AdminIndex");
            }
            return View(booking);
        }

        // Delete booking
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var booking = _bookingRepo.GetBookingById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int packageId)
        {
            // Delete the booking
            _bookingRepo.DeleteBooking(id);

            // Redirect back to the same package's bookings page
            return RedirectToAction("PackageBookings", new { packageId = packageId });
        }


        // Inline delete from package admin view
        [HttpPost]
        public IActionResult DeleteFromPackage(int id, int packageId)
        {
            _bookingRepo.DeleteBooking(id);
            return RedirectToAction("AdminIndex", "Package");
        }


        [HttpGet]
        public IActionResult PackageBookings(int packageId)
        {
            var bookings = _bookingRepo.GetAllBookings()
                                       .Where(b => b.PackageId == packageId)
                                       .ToList();

            var package = _packageRepo.GetById(packageId);
            ViewBag.PackageName = package?.PackageName;

            return View(bookings); // Create PackageBookings.cshtml
        }

        [HttpGet]
        public IActionResult CancelledBookings()
        {
            var cancelledBookings = _bookingRepo.GetAllBookings()
                                                .Where(b => b.Status == "CANCELLED")
                                                .ToList();

            return View(cancelledBookings); // Create CancelledBookings.cshtml
        }

    }
}
