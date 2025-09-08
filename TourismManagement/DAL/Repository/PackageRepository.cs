using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.DAL.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly TourismDBContext context;

        public PackageRepository(TourismDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Package> GetAll() => context.Packages.ToList();

        public Package GetById(int id)
        {
            var pkg = context.Packages.Find(id);
            if (pkg == null) throw new Exception($"Package with ID {id} not found.");
            return pkg;
        }

        public void Add(Package package)
        {
            context.Packages.Add(package);
        }

        public void Update(Package package)
        {
            context.Packages.Update(package);
        }

        public void Delete(int id)
        {
            var pkg = context.Packages.Find(id);
            if (pkg == null) throw new Exception($"Package with ID {id} not found for deletion.");
            context.Packages.Remove(pkg);
        }

        public void Save() => context.SaveChanges();
    }
}

