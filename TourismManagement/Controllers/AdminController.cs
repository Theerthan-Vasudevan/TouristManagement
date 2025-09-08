using Microsoft.AspNetCore.Mvc;
using TourismManagement.NewFolder;
using TourismManagement.Service;

namespace TourismManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly PackageService packageService;
        private readonly BookingService bookingService;
        private readonly UserService userService;

        public AdminController(PackageService packageService, BookingService bookingService, UserService userService)
        {
            this.packageService = packageService;
            this.bookingService = bookingService;
            this.userService = userService;
        }

        public ActionResult Dashboard()
        {
            ViewBag.TotalUsers = userService.GetAllUsers().Count();
            ViewBag.TotalBookings = bookingService.GetAllBookings().Count();
            ViewBag.TotalPackages = packageService.GetAllPackages().Count();
            return View();
        }

        public ActionResult ManagePackages()
        {
            var packages = packageService.GetAllPackages();
            return View(packages);
        }

        public ActionResult ManageBookings()
        {
            var bookings = bookingService.GetAllBookings();
            return View(bookings);
        }

        public ActionResult Reports()
        {
            var report = bookingService.GenerateReport();
            return View(report);
        }
    }
}
