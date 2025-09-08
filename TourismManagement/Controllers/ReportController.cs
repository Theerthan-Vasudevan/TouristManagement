using Microsoft.AspNetCore.Mvc;

namespace TourismManagement.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
