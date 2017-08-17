using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly BusinessDbContext _context;

        public OrderItemRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public OrderItem AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return orderItem;
        }

        public void DeleteOrderItem(int orderId, int productId)
        {
            var item = _context.OrderItems.Where(oi => oi.OrderId == orderId && oi.ProductId == productId).FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }

        public OrderItem GetOrderItem(int orderId, int productId)
        {
            return _context.OrderItems
                .Where(oi => oi.OrderId == orderId && oi.ProductId == productId)
                .FirstOrDefault();
        }

        public IQueryable<OrderItem> GetOrderItems()
        {
            return _context.OrderItems.AsQueryable();
        }

        public OrderItem UpdateOrderItem(OrderItem orderItem)
        {
            var oldItem = _context.OrderItems
                .FirstOrDefault(oi => oi.OrderId == orderItem.OrderId && oi.ProductId == orderItem.ProductId);
            _context.Entry(oldItem).CurrentValues.SetValues(orderItem);
            _context.SaveChanges();
            return orderItem;
        }
    }
}
