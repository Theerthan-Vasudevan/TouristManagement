using Microsoft.AspNetCore.Mvc;
using TourismManagement.Models;
using TourismManagement.NewFolder;

namespace TourismManagement.Controllers
{
    public class PackageController : Controller
    {
        private readonly PackageService packageService;

        public PackageController(PackageService packageService)
        {
            this.packageService = packageService;
        }

        public ActionResult Index()
        {
            var packages = packageService.GetAllPackages();
            return View(packages);
        }

        public ActionResult Details(int id)
        {
            var package = packageService.GetPackageById(id);
            return View(package);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Package package)
        {
            if (ModelState.IsValid)
            {
                packageService.CreatePackage(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var package = packageService.GetPackageById(id);
            return View(package);
        }

        [HttpPost]
        public ActionResult Edit(Package package)
        {
            if (ModelState.IsValid)
            {
                packageService.UpdatePackage(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

        public ActionResult Delete(int id)
        {
            packageService.DeletePackage(id);
            return RedirectToAction("Index");
        }
    }
}
