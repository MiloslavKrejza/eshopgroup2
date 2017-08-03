using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Context;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class FormatRepository : IFormatRepository
    {
        private readonly CatalogueDbContext _context;

        public FormatRepository(CatalogueDbContext context)
        {
            _context = context;
        }

        public Format AddFormat(Format format)
        {
            _context.Formats.Add(format);
            _context.SaveChanges();
            return format;
        }

        public void DeleteFormat(int id)
        {
            var format = _context.Formats.FirstOrDefault(f => f.Id == id);
            _context.Formats.Remove(format);
            _context.SaveChanges();
        }

        public IQueryable<Format> GetAllFormats()
        {
            return _context.Formats.AsQueryable();
        }

        public Format GetFormat(int id)
        {
            return _context.Formats.FirstOrDefault(f => f.Id == id);
        }

        public Format UpdateFormat(Format format)
        {
            var oldFormat = GetFormat(format.Id);
            _context.Entry(oldFormat).CurrentValues.SetValues(format);
            _context.SaveChanges();
            return format;
        }
    }
}
