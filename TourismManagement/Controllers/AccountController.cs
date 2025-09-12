using Microsoft.AspNetCore.Mvc;
using TourismManagement.Models;
using TourismManagement.Service;

namespace TourismManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService userService;

        public AccountController(UserService userService)
        {
            if (userService == null) throw new Exception("UserService not injected!");
            this.userService = userService;
        }

        // Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Username = model.Username,
                    PasswordHash = userService.HashPassword(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    Role = "Customer"
                };

                userService.RegisterUser(user);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var user = userService.Authenticate(username, password);

                // Store in Session (ASP.NET Core way)
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                    return RedirectToAction("Dashboard", "Admin");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
