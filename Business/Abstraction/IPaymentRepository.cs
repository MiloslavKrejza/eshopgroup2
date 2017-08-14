using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    interface IPaymentRepository
    {
        /// <summary>
        /// Gets a Payment type
        /// </summary>
        /// <param name="paymentId">Payment id</param>
        /// <returns>Payment of a product</returns>
        Payment GetPayment(int paymentId);

        /// <summary>
        /// Gets all Payment types
        /// </summary>
        /// <returns>IQueryable of Payment</returns>
        IQueryable<Payment> GetPayments();

        /// <summary>
        /// Adds a new Payment type
        /// </summary>
        /// <param name="paymentId">New Payment type</param>
        /// <returns>Added Payment</returns> 
        Payment AddPayment(Payment payment);

        /// <summary>
        /// Updates a Payment type with new data
        /// </summary>
        /// <param name="payment">Payment data to update</param>
        /// <returns>Updated Payment</returns>
        Payment UpdatePayment(Payment payment);

        /// <summary>
        /// Deletes a Payment type with specified PaymentId
        /// </summary>
        /// <param name="paymentId">Payment identifier</param>
        void DeletePayment(int paymentId);
    }
}
