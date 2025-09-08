using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.NewFolder
{
    public class PackageService
    {
        private readonly IPackageRepository packageRepo;

        public PackageService(IPackageRepository packageRepo)
        {
            this.packageRepo = packageRepo;
        }

        public IEnumerable<Package> GetAllPackages()
        {
            return packageRepo.GetAll();
        }

        public Package GetPackageById(int id)
        {
            return packageRepo.GetById(id);
        }

        public void CreatePackage(Package package)
        {
            packageRepo.Add(package);
            packageRepo.Save();
        }

        public void UpdatePackage(Package package)
        {
            packageRepo.Update(package);
            packageRepo.Save();
        }

        public void DeletePackage(int id)
        {
            packageRepo.Delete(id);
            packageRepo.Save();
        }
    }
}
