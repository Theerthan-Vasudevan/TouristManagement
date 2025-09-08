using Microsoft.EntityFrameworkCore;
using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.DAL.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly TourismDBContext context;

        public PaymentRepository(TourismDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Payment> GetAll() =>
            context.Payments.Include("Booking").ToList();

        public Payment GetById(int id)
        {
            var payment = context.Payments
                                 .Include("Booking")
                                 .FirstOrDefault(p => p.PaymentId == id);
            if (payment == null) throw new Exception($"Payment with ID {id} not found.");
            return payment;
        }

        public Payment GetByBookingId(int bookingId)
        {
            var payment = context.Payments.FirstOrDefault(p => p.BookingId == bookingId);
            if (payment == null) throw new Exception($"Payment for Booking ID {bookingId} not found.");
            return payment;
        }

        public void Add(Payment payment)
        {
            context.Payments.Add(payment);
        }

        public void Update(Payment payment)
        {
            context.Payments.Update(payment);
        }

        public void Delete(int id)
        {
            var payment = context.Payments.Find(id);
            if (payment == null) throw new Exception($"Payment with ID {id} not found for deletion.");
            context.Payments.Remove(payment);
        }

        public void Save() => context.SaveChanges();
    }
}
