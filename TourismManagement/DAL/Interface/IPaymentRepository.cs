using TourismManagement.Models;

namespace TourismManagement.DAL.Interface
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
        Payment GetByBookingId(int bookingId);
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(int id);
        void Save();
    }
}
