using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BusinessDbContext _context;

        public PaymentRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public Payment AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(c => c.Id == id);
            _context.Payments.Remove(payment);
            _context.SaveChanges();
        }

        public IQueryable<Payment> GetPayments()
        {
            return _context.Payments.AsQueryable();
        }

        public Payment GetPayment(int id)
        {
            var payment = _context.Payments
                .FirstOrDefault(p => p.Id == id);
            return payment;
        }

        public Payment UpdatePayment(Payment payment)
        {
            var oldPayment = _context.Payments.FirstOrDefault(p => p.Id == payment.Id);
            _context.Entry(oldPayment).CurrentValues.SetValues(payment);
            _context.SaveChanges();
            return payment;
        }
    }
}
