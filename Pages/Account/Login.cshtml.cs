using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TourismManagement.Service;

namespace TourismManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserService userService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(UserService userService)
        {
            this.userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
                var user = userService.Authenticate(Username, Password);

                // Example: Set session (if configured)
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                    return RedirectToPage("/Admin/Dashboard");

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}