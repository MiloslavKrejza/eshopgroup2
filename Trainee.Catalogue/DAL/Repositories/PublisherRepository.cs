using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Context;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly CatalogueDbContext _context;

        public PublisherRepository(CatalogueDbContext context)
        {
            _context = context;
        }


        public Publisher AddPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }

        public void DeletePublisher(int id)
        {
            var publ = _context.Publishers.FirstOrDefault(p => p.Id == id);
            _context.Publishers.Remove(publ);
            _context.SaveChanges();
        }

        public IQueryable<Publisher> GetAllPublishers()
        {
            return _context.Publishers.AsQueryable();
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
        }

        public Publisher UpdatePublisher(Publisher publisher)
        {
            var oldPubl = GetPublisher(publisher.Id);
            _context.Entry(oldPubl).CurrentValues.SetValues(publisher);
            _context.SaveChanges();
            return publisher;
        }
    }
}
