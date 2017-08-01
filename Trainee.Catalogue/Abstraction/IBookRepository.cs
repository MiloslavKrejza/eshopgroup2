using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    public interface IBookRepository
    {
        /// <summary>
        /// Adds a new Book to the database
        /// </summary>
        /// <param name="book">Book to be added</param>
        /// <returns>Added book</returns>
        Book AddBook(Book book);

        /// <summary>
        /// Gets a book with the specified id
        /// </summary>
        /// <param name="id">Id of the book to be get</param>
        /// <returns>Boook with the specified id</returns>
        Book GetBook(int id);

        /// <summary>
        /// Gets an IQueryable of all books
        /// </summary>
        /// <returns>IQueryable of Book</returns>
        IQueryable<Book> GetAllBooks();

        /// <summary>
        /// Updates a Book in the database
        /// </summary>
        /// <param name="book">Book to be updated</param>
        /// <returns>Updated book</returns>
        Book UpdateBook(Book book);

        /// <summary>
        /// Deletes a Book with the specified id
        /// </summary>
        /// <param name="id">Book's id</param>
        void DeleteBook(int id);
    }
}
