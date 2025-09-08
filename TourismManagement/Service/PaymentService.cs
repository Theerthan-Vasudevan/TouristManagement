using TourismManagement.DAL.Interface;
using TourismManagement.Models;

namespace TourismManagement.NewFolder
{
    public class PaymentService
    {
        private readonly IPaymentRepository paymentRepo;
        private readonly IBookingRepository bookingRepo;

        public PaymentService(IPaymentRepository paymentRepo, IBookingRepository bookingRepo)
        {
            this.paymentRepo = paymentRepo;
            this.bookingRepo = bookingRepo;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return paymentRepo.GetAll();
        }

        public Payment GetPaymentById(int id)
        {
            return paymentRepo.GetById(id);
        }

        public Payment GetPaymentByBookingId(int bookingId)
        {
            return paymentRepo.GetByBookingId(bookingId);
        }

        public void MakePayment(int bookingId, decimal amount, string method)
        {
            var booking = bookingRepo.GetById(bookingId);
            if (booking == null || booking.Status == "Cancelled")
                throw new Exception("Invalid booking for payment.");

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentMethod = method,
                Status = "Paid",
                PaymentDate = DateTime.Now
            };

            paymentRepo.Add(payment);
            paymentRepo.Save();
        }

        public void RefundPayment(int paymentId)
        {
            var payment = paymentRepo.GetById(paymentId);
            payment.Status = "Refunded";

            paymentRepo.Update(payment);
            paymentRepo.Save();
        }
    }
}
