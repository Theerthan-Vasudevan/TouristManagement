using Microsoft.AspNetCore.Mvc;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;
using System.Linq;

namespace TourismManagementV2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // ✅ Registration Page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            user.Role = "User";
            if (ModelState.IsValid)
            {
                _userRepo.addUser(user);
                return RedirectToAction("Login", "Home");
            }
            return View(user);
        }

        // Admin view to list all users
        public IActionResult AdminIndex()
        {
            var users = _userRepo.getAllUsers().ToList();
            return View(users); // expects List<User>
        }

        // GET: User/Profile
        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            var user = _userRepo.findById(userId.Value); // single user
            if (user == null)
                return NotFound();

            return View(user); // passes single User
        }

        // GET: User/EditProfile
        [HttpGet]
        public IActionResult EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            var user = _userRepo.findById(userId.Value);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(User user)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            // Fetch the existing tracked entity from DB
            var existingUser = _userRepo.findById(userId.Value);
            if (existingUser == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Update only the fields that came from the form
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;

                // Password: only update if a new password was entered
                if (!string.IsNullOrEmpty(user.Password))
                {
                    existingUser.Password = user.Password;
                }

                // Save changes
                _userRepo.updateUser(existingUser); // now updates the tracked entity

                return RedirectToAction("UserIndex","Package");
            }

            return View(user);
        }




        // GET: User/DeleteProfile
        [HttpGet]
        public IActionResult DeleteProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            var user = _userRepo.findById(userId.Value);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: User/DeleteProfile
        [HttpPost, ActionName("DeleteProfile")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProfileConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Home");

            _userRepo.deleteUser(id);
            //HttpContext.Session.Clear(); // log out after deletion
            return RedirectToAction("AdminIndex", "User");
        }

        // GET: User/UserIndex (for general list of users)
        public IActionResult UserIndex()
        {
            var users = _userRepo.getAllUsers(); // List<User>
            return View(users);
        }

        // GET: Admin/Edit user by ID
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userRepo.findById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Admin/Edit user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.updateUser(user);
                return RedirectToAction("UserIndex");
            }
            return View(user);
        }

        // GET: Admin/Delete user
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _userRepo.findById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Admin/Delete user
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepo.deleteUser(id);
            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            // Redirect to login or home page
            return RedirectToAction("Login", "Home");
        }

    }
}
