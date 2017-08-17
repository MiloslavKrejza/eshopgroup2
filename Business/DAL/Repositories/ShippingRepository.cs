using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly BusinessDbContext _context;

        public ShippingRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public Shipping AddShipping(Shipping shipping)
        {
            _context.Shippings.Add(shipping);
            _context.SaveChanges();
            return shipping;
        }

        public void DeleteShipping(int id)
        {
            var shipping = _context.Shippings.FirstOrDefault(s => s.Id == id);
            _context.Shippings.Remove(shipping);
            _context.SaveChanges();
        }

        public IQueryable<Shipping> GetShippings()
        {
            return _context.Shippings.AsQueryable();
        }

        public Shipping GetShipping(int id)
        {
            var shipping = _context.Shippings
                .FirstOrDefault(s => s.Id == id);
            return shipping;
        }

        public Shipping UpdateShipping(Shipping shipping)
        {
            var oldShipping = _context.Shippings.FirstOrDefault(s => s.Id == shipping.Id);
            _context.Entry(oldShipping).CurrentValues.SetValues(shipping);
            _context.SaveChanges();
            return shipping;
        }
    }
}
