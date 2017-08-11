using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    /// <summary>
    /// This repository provides CRUD operations for authors
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Adds a new Author
        /// </summary>
        /// <param name="author">Author to be added</param>
        /// <returns>Added author</returns>
        Author AddAuthor(Author author);

        /// <summary>
        /// Provides an Author entity.
        /// </summary>
        /// <param name="id">Author's unique identifier</param>
        /// <returns>Author with the specified id</returns>
        Author GetAuthor(int id);

        /// <summary>
        /// Gets an IQueryable object of all authors
        /// </summary>
        /// <returns>IQueryable of authors</returns>
        IQueryable<Author> GetAllAuthors();

        /// <summary>
        /// Updates an Author in the database
        /// </summary>
        /// <param name="author">Author to be updated</param>
        /// <returns>Updated author profile</returns>
        Author UpdateAuthor(Author author);

        /// <summary>
        /// Deletes an Author with the specified id
        /// </summary>
        /// <param name="id">Author's identifier</param>
        void DeleteAuthor(int id);
    }
}
