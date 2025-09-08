using TourismManagement.Models;

namespace TourismManagement.DAL.Interface
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(int id);
        IEnumerable<Booking> GetByUserId(int userId);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(int id);
        void Save();
    }
}
