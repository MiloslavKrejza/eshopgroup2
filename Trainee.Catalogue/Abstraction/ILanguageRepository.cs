using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    public interface ILanguageRepository
    {
        /// <summary>
        /// Adds a new Language
        /// </summary>
        /// <param name="lang">Language to be added</param>
        /// <returns>Added Language</returns>
        Language AddLanguage(Language lang);

        /// <summary>
        /// Gets a Language with the specified id
        /// </summary>
        /// <param name="id">Id of the language</param>
        /// <returns>Language with the specified id</returns>
        Language GetLanguage(int id);

        /// <summary>
        /// Gets an IQueryable of all Languages
        /// </summary>
        /// <returns>IQueryable of all Languages</returns>
        IQueryable<Language> GetAllLangs();

        /// <summary>
        /// Updates a Language
        /// </summary>
        /// <param name="lang">Language to be updated</param>
        /// <returns>Updated Language</returns>
        Language UpdateLanguage(Language lang);

        /// <summary>
        /// Deletes a Language with the specified id
        /// </summary>
        /// <param name="id">Id of the Language to be deleted</param>
        void DeleteLanguage(int id);
    }
}
