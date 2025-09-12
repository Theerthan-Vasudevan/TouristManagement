using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;
using Microsoft.AspNetCore.Hosting;

namespace TourismManagementV2.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageRepository dal;
        private readonly IBookingRepository _bookingRepo;
        private readonly IWebHostEnvironment _env;

        public PackageController(IPackageRepository dal, IBookingRepository bookingRepo, IWebHostEnvironment env)
        {
            this.dal = dal;
            _bookingRepo = bookingRepo;
            _env = env;
        }

        // Admin view
        public IActionResult AdminIndex()
        {
            var packages = dal.GetAll().ToList();
            packages = ValidateImages(packages);
            return View(packages);
        }

        // User view
        public IActionResult UserIndex(string searchString)
        {
            // Fetch real packages from DB
            var packages = dal.GetAll().ToList();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                packages = packages
                    .Where(p => p.PackageName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Validate images (your existing method)
            packages = ValidateImages(packages);

            return View(packages);
        }



        // Helper method to check if image exists, else set fallback
        private List<Package> ValidateImages(List<Package> packages)
        {
            foreach (var pkg in packages)
            {
                if (!string.IsNullOrEmpty(pkg.ImageUrl))
                {
                    var filePath = Path.Combine(_env.WebRootPath, pkg.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (!System.IO.File.Exists(filePath))
                    {
                        pkg.ImageUrl = "/images/no-image.png";
                    }
                }
                else
                {
                    pkg.ImageUrl = "/images/no-image.png";
                }
            }
            return packages;
        }

        // Create (Admin only)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    package.ImageUrl = "/images/" + fileName;
                }

                dal.Add(package);
                return RedirectToAction("AdminIndex");
            }
            return View(package);
        }

        // Edit (Admin only)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var package = dal.GetById(id);
            if (package == null)
                return NotFound();
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Package package, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    package.ImageUrl = "/images/" + fileName;
                }

                dal.Update(package);
                return RedirectToAction("AdminIndex");
            }
            return View(package);
        }

        // Delete (Admin only)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var package = dal.GetById(id);
            if (package == null)
                return NotFound();
            return View(package);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            dal.Delete(id);
            return RedirectToAction("AdminIndex");
        }

        // Details (for both Admin & User)
        public IActionResult Details(int id)
        {
            var package = dal.GetById(id);
            if (package == null)
                return NotFound();

            // Validate image for details view as well
            if (!string.IsNullOrEmpty(package.ImageUrl))
            {
                var filePath = Path.Combine(_env.WebRootPath, package.ImageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (!System.IO.File.Exists(filePath))
                {
                    package.ImageUrl = "/images/no-image.png";
                }
            }
            else
            {
                package.ImageUrl = "/images/no-image.png";
            }

            return View(package);
        }
    }
}
