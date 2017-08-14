using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    class OrderRepository : IOrderRepository
    {
        public Order AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Order UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
