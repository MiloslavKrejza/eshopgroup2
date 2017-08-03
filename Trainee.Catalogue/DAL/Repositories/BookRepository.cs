using System;
using System.Linq;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Catalogue.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Trainee.Catalogue.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly CatalogueDbContext _context;
        public BookRepository(CatalogueDbContext context)
        {
            _context = context;
        }
        public Book AddBook(Book book)
        {

            _context.Books.Add(book);
            _context.SaveChanges();
            return book;

        }

        public void DeleteBook(int id)
        {

            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            _context.Remove(book);
            _context.SaveChanges();

        }

        public IQueryable<Book> GetAllBooks()
        {
            return _context.Books.AsQueryable();
        }

        public Book GetBook(int id)
        {
            var result = _context.Books
                .Where(b => b.Id == id)
                .Include(b => b.AuthorsBooks)
                    .ThenInclude(ab => ab.Author)
                .FirstOrDefault();
            return result;
        }

        public Book UpdateBook(Book book)
        {
            var oldBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            _context.Entry(oldBook).CurrentValues.SetValues(book);
            return book;
        }
    }
}
