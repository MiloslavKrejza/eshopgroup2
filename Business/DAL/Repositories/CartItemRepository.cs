using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly BusinessDbContext _context;

        public CartItemRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public CartItem AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return cartItem;
        }

        public void DeleteCartItem(string visitorId, int productId)
        {
            var item = _context.CartItems.Where(ci => ci.VisitorId == visitorId && ci.ProductId == productId).FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }

        public CartItem GetCartItem(string visitorId, int productId)
        {
            return _context.CartItems
                .Where(ci => ci.VisitorId == visitorId && ci.ProductId == productId)
                .FirstOrDefault();
        }

        public IQueryable<CartItem> GetCartItems()
        {
            return _context.CartItems.AsQueryable();
        }

        public CartItem UpdateCartItem(CartItem cartItem)
        {
            var oldItem = _context.CartItems
                .FirstOrDefault(ci => ci.VisitorId == cartItem.VisitorId && ci.ProductId == cartItem.ProductId);
            _context.Entry(oldItem).CurrentValues.SetValues(cartItem);
            _context.SaveChanges();
            return cartItem;
        }
    }
}
