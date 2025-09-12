using Microsoft.EntityFrameworkCore;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TourismManagementV2.DAL.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly TourismDBContext context;

        public PackageRepository(TourismDBContext context)
        {
            this.context = context;
        }

        // Add a new package
        public void Add(Package package)
        {
            context.Packages.Add(package);
            context.SaveChanges();
        }

        // Delete a package by Id
        public void Delete(int id)
        {
            var package = context.Packages.Find(id);
            if (package != null)
            {
                context.Packages.Remove(package);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Package not found!");
            }
        }

        // Get all packages including bookings (UserId & Status)
        public List<Package> GetAll()
        {
            return context.Packages
                .Include(p => p.Bookings)
                .Select(p => new Package
                {
                    PackageId = p.PackageId,
                    PackageName = p.PackageName,
                    Description = p.Description,
                    Price = p.Price,
                    DurationDays = p.DurationDays,
                    ImageUrl = p.ImageUrl, // <-- add this line
                    Bookings = p.Bookings
                        .Select(b => new Booking
                        {
                            BookingId = b.BookingId,
                            UserId = b.UserId,
                            Status = b.Status
                        }).ToList()
                })
                .ToList();
        }


        // Get package by Id including bookings
        public Package GetById(int id)
        {
            var package = context.Packages
                .Include(p => p.Bookings)
                .FirstOrDefault(p => p.PackageId == id);

            if (package != null)
                return package;

            throw new Exception("Package not found!");
        }

        // Update package
        public void Update(Package package)
        {
            context.Packages.Update(package);
            context.SaveChanges();
        }

        // Optional Save method (can be removed if always saving immediately)
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
