using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BusinessDbContext _context;

        public OrderRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IQueryable<Order> GetOrders()
        {
            return _context.Orders.AsQueryable();
        }

        public Order GetOrder(int id)
        {
            //todo add products in service
            var order = _context.Orders
                .Include(o => o.Country)
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .Include(o => o.OrderState)
                .FirstOrDefault(os => os.Id == id);
            return order;
        }

        public Order UpdateOrder(Order order)
        {
            var oldOrder = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            _context.Entry(oldOrder).CurrentValues.SetValues(order);
            _context.SaveChanges();
            return order;
        }
    }
}
