using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    public interface IPublisherRepository
    {
        /// <summary>
        /// Creates a new Publisher
        /// </summary>
        /// <param name="publisher">Publisher to be added</param>
        /// <returns>Added Publisher</returns>
        Publisher AddPublisher(Publisher publisher);

        /// <summary>
        /// Provides an Publisher entity.
        /// </summary>
        /// <param name="id">Publisher's unique identifier</param>
        /// <returns>Publisher with the specified id</returns>
        Publisher GetPublisher(int id);

        /// <summary>
        /// Gets an IQueryable object of all Publishers
        /// </summary>
        /// <returns>IQueryable of Publishers</returns>
        IQueryable<Publisher> GetAllPublishers();

        /// <summary>
        /// Updates an Publisher in the database
        /// </summary>
        /// <param name="publisher">Publisher to be updated</param>
        /// <returns>Updated Publisher profile</returns>
        Publisher UpdatePublisher(Publisher publisher);

        /// <summary>
        /// Deletes an Publisher with the specified id
        /// </summary>
        /// <param name="id">Publisher's identifier</param>
        void DeletePublisher(int id);
    }
}
