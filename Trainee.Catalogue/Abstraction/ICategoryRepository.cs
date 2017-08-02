using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Adds a new Category to the database
        /// </summary>
        /// <param name="category">Category to be added</param>
        /// <returns>Added Category</returns>
        Category AddCategory(Category category);

        /// <summary>
        /// Gets a Category with the specified id
        /// </summary>
        /// <param name="id">Id of the Category to be get</param>
        /// <returns>Boook with the specified id</returns>
        Category GetCategory(int id);

        /// <summary>
        /// Gets an IQueryable of all Categoriess
        /// </summary>
        /// <returns>IQueryable of Categories</returns>
        IQueryable<Category> GetAllCategories();

        /// <summary>
        /// Updates a Category in the database
        /// </summary>
        /// <param name="category">Category to be updated</param>
        /// <returns>Updated Category</returns>
        Category UpdateCategory(Category category);

        /// <summary>
        /// Deletes a Category with the specified id
        /// </summary>
        /// <param name="id">Category id</param>
        void DeleteCategory(int id);
    }
}
