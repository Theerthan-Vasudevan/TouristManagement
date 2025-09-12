using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TourismManagement.Models;

namespace TourismManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Redirect directly to the Login page
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult GetStarted()
        {
            return View();
        }

    }
}
