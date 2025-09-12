using TourismManagementV2.Models;

namespace TourismManagementV2.DAL.Interface
{
    public interface IPackageRepository
    {
        List<Package> GetAll();
        Package GetById(int id);
        void Add(Package package);
        void Update(Package package);
        void Delete(int id);
        void Save();
    }
}
