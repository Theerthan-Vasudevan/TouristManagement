using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TourismManagement.Models;

namespace TourismManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
    }
}
