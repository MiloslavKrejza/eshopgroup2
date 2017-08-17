using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IOrderItemRepository
    {
        /// <summary>
        /// Gets an OrderItem
        /// </summary>
        /// <param name="productId">Item Product id</param>
        /// <param name="orderId">Order id</param>
        OrderItem GetOrderItem(int orderId, int productId);

        /// <summary>
        /// Gets all OrderItems
        /// </summary>
        /// <returns>IQueryable of OrderItem</returns>
        IQueryable<OrderItem> GetOrderItems();

        /// <summary>
        /// Adds a new OrderItem
        /// </summary>
        /// <param name="orderItemId">New OrderItem</param>
        /// <returns>Added OrderItem</returns> 
        OrderItem AddOrderItem(OrderItem orderItem);

        /// <summary>
        /// Updates an OrderItem with new data
        /// </summary>
        /// <param name="orderItem">OrderItem data to update</param>
        /// <returns>Updated OrderItem</returns>
        OrderItem UpdateOrderItem(OrderItem orderItem);

        /// <summary>
        /// Deletes an OrderItem with specified OrderItemId
        /// </summary>
        /// <param name="productId">Item product id</param>
        /// <param name="orderId">Order id</param>
        void DeleteOrderItem(int orderId, int productId);
    }
}

