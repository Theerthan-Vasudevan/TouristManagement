using TourismManagement.Models;

namespace TourismManagement.DAL.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        void Save();
    }
}
