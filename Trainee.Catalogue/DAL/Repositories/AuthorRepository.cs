using System;
using System.Linq;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Catalogue.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
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

            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == id);
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
                .Where(a => a.AuthorId == id)
                .Include(a => a.Country)
                //.Include(a => a.AuthorsBooks)
                   // .ThenInclude(ab => ab.Book)
                .FirstOrDefault();

            result.AuthorsBooks = _context.AuthorsBooks.Where(ab => ab.AuthorId == result.AuthorId).ToList();

            foreach(AuthorBook ab in result.AuthorsBooks)
            {
                ab.Book = _context.Books.Where(b => b.BookId == ab.BookId).FirstOrDefault();
            }

            return result;

        }

        public Author UpdateAuthor(Author author)
        {

            var oldAuthor = GetAuthor(author.AuthorId);
            _context.Entry(oldAuthor).CurrentValues.SetValues(author);
            _context.SaveChanges();
            return author;

        }
    }
}
