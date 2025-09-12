using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;

namespace TourismManagementV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;

        public HomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Please enter both username and password.";
                return View();
            }

            // Hardcoded admin credentials
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetInt32("UserId", 0); // dummy Id for admin
                HttpContext.Session.SetString("Role", "Admin");
                return RedirectToAction("UserIndex", "Package");
            }

            // Check user in the database
            var user = userRepository.getAllUsers()
                                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }

            // Login successful
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("UserIndex", "Package");
        }




        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
