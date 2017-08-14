using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    interface IOrderRepository
    {
        /// <summary>
        /// Gets an Order
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Order by id</returns>
        Order GetOrder(int orderId);

        /// <summary>
        /// Gets all Orders
        /// </summary>
        /// <returns>IQueryable of Order</returns>
        IQueryable<Order> GetOrders();

        /// <summary>
        /// Adds a new Order
        /// </summary>
        /// <param name="orderId">New Order</param>
        /// <returns>Added Order</returns> 
        Order AddOrder(Order order);

        /// <summary>
        /// Updates an Order with new data
        /// </summary>
        /// <param name="order">Order data to update</param>
        /// <returns>Updated Order</returns>
        Order UpdateOrder(Order order);

        /// <summary>
        /// Deletes an Order with specified OrderId
        /// </summary>
        /// <param name="orderId">Order identifier</param>
        void DeleteOrder(int orderId);
    }
}
