using TourismManagement.Models;

namespace TourismManagement.DAL.Interface
{
    public interface IPackageRepository
    {
        IEnumerable<Package> GetAll();
        Package GetById(int id);
        void Add(Package package);
        void Update(Package package);
        void Delete(int id);
        void Save();
    }
}
