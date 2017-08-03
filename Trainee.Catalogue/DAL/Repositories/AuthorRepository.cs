using System;
using System.Linq;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Catalogue.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Trainee.Catalogue.DAL.Repositories
{
    class AuthorRepository : IAuthorRepository
    {
        private readonly CatalogueDbContext _context;

        public AuthorRepository(CatalogueDbContext context)
        {
            _context = context;
        }

        public Author AddAuthor(Author author)
        {

            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;

        }

        public void DeleteAuthor(int id)
        {

            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            _context.Authors.Remove(author);
            _context.SaveChanges();

        }

        public IQueryable<Author> GetAllAuthors()
        {
            return _context.Authors.AsQueryable();
        }

        public Author GetAuthor(int id)
        {

            var result = _context.Authors
                .Where(a => a.Id == id)
                .Include(a => a.Country)
                .Include(a => a.AuthorsBooks)
                    .ThenInclude(ab => ab.Book)
                .FirstOrDefault();

            return result;

        }

        public Author UpdateAuthor(Author author)
        {

            var oldAuthor = GetAuthor(author.Id);
            _context.Entry(oldAuthor).CurrentValues.SetValues(author);
            _context.SaveChanges();
            return author;

        }
    }
}
