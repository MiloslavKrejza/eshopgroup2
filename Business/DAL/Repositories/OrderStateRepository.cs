using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Trainee.Business.DAL.Context;
using System.Linq;
using Trainee.Business.DAL.Entities;
using Trainee.Business.Abstraction;

namespace Trainee.Business.DAL.Repositories
{
   public class OrderStateRepository : IOrderStateRepository
    {
        private readonly BusinessDbContext _context;

        public OrderStateRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public OrderState AddOrderState(OrderState orderState)
        {
            _context.OrderStates.Add(orderState);
            _context.SaveChanges();
            return orderState;
        }

        public void DeleteOrderState(int id)
        {
            var orderState = _context.OrderStates.FirstOrDefault(os => os.Id == id);
            _context.OrderStates.Remove(orderState);
            _context.SaveChanges();
        }

        public IQueryable<OrderState> GetOrderStates()
        {
            return _context.OrderStates.AsQueryable();
        }

        public OrderState GetOrderState(int id)
        {
            var orderState = _context.OrderStates
                .FirstOrDefault(os => os.Id == id);
            return orderState;
        }

        public OrderState UpdateOrderState(OrderState orderState)
        {
            var oldOrderState = _context.OrderStates.FirstOrDefault(os => os.Id == orderState.Id);
            _context.Entry(oldOrderState).CurrentValues.SetValues(orderState);
            _context.SaveChanges();
            return orderState;
        }
    }
}
