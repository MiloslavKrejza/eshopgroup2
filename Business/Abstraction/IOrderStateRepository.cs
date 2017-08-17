using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IOrderStateRepository
    {
        /// <summary>
        /// Gets an OrderState
        /// </summary>
        /// <param name="orderStateId">Order state id</param>
        /// <returns>OrderState</returns>
        OrderState GetOrderState(int orderStateId);

        /// <summary>
        /// Gets all OrderStates
        /// </summary>
        /// <returns>IQueryable of OrderState</returns>
        IQueryable<OrderState> GetOrderStates();

        /// <summary>
        /// Adds a new OrderState
        /// </summary>
        /// <param name="orderStateId">New OrderState</param>
        /// <returns>Added OrderState</returns> 
        OrderState AddOrderState(OrderState orderState);

        /// <summary>
        /// Updates an OrderState with new data
        /// </summary>
        /// <param name="orderState">OrderState data to update</param>
        /// <returns>Updated OrderState</returns>
        OrderState UpdateOrderState(OrderState orderState);

        /// <summary>
        /// Deletes an OrderState with specified OrderStateId
        /// </summary>
        /// <param name="orderStateId">OrderState identifier</param>
        void DeleteOrderState(int orderStateId);
    }
}
