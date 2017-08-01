using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    public interface IFormatRepository
    {
        /// <summary>
        /// Creates a new Format
        /// </summary>
        /// <param name="Format">Format to be added</param>
        /// <returns>Added Format</returns>
        Format AddFormat(Format format);

        /// <summary>
        /// Provides an Format entity.
        /// </summary>
        /// <param name="id">Format's unique identifier</param>
        /// <returns>Format with the specified id</returns>
        Format GetFormat(int id);

        /// <summary>
        /// Gets an IQueryable object of all Formats
        /// </summary>
        /// <returns>IQueryable of Formats</returns>
        IQueryable<Format> GetAllFormats();

        /// <summary>
        /// Updates an Format in the database
        /// </summary>
        /// <param name="Format">Format to be updated</param>
        /// <returns>Updated Format profile</returns>
        Format UpdateFormat(Format format);

        /// <summary>
        /// Deletes an Format with the specified id
        /// </summary>
        /// <param name="id">Format's identifier</param>
        void DeleteFormat(int id);
    }
}
