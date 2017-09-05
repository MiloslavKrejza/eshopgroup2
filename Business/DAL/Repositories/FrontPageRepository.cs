using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    public class FrontPageRepository : IFrontPageRepository
    {
        private readonly BusinessDbContext _context;

        public FrontPageRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public FrontPageItem AddFrontPageItem(FrontPageItem item)
        {
            _context.FrontPageItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void DeleteFrontPageItem(int id)
        {
            var item = _context.FrontPageItems.Where(fi => fi.Id == id).FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }

        public FrontPageItem GetFrontPageItem(int id)
        {
            return _context.FrontPageItems.FirstOrDefault(fi => fi.Id == id);
        }

        public IQueryable<FrontPageSlot> GetSlotItems()
        {
            var slots = _context.FrontPageSlots.ToList();
            var ids = slots.Select(s => s.ItemId);

            var items = _context.FrontPageItems.Where(fi => ids.Contains(fi.Id)).AsQueryable();
            return slots.Join(items, s => s.ItemId, fi => fi.Id, (s, fi) => { s.Item = fi; return s; }).AsQueryable();
        }

        public IQueryable<FrontPageItem> GetFrontPageItems()
        {
            return _context.FrontPageItems.AsQueryable();
        }

        public FrontPageItem UpdateFrontPageItem(FrontPageItem item)
        {
            var oldItem = _context.FrontPageItems
                .FirstOrDefault(fi => fi.Id == item.Id);
            _context.Entry(oldItem).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return item;
        }
    }
}
