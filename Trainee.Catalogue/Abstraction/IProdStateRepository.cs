using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    interface IProdStateRepository
    {
        /// <summary>
        /// Adds a new ProductState to the database
        /// </summary>
        /// <param name="productState">ProductState to be added</param>
        /// <returns>Added ProductState</returns>
        ProductState AddProductState(ProductState productState);

        /// <summary>
        /// Gets a ProductState with the specified id
        /// </summary>
        /// <param name="id">Id of the ProductState to be get</param>
        /// <returns>Boook with the specified id</returns>
        ProductState GetProductState(int id);

        /// <summary>
        /// Gets an IQueryable of all ProductStates
        /// </summary>
        /// <returns>IQueryable of ProductState</returns>
        IQueryable<ProductState> GetAllProductStates();

        /// <summary>
        /// Updates a ProductState in the database
        /// </summary>
        /// <param name="productState">ProductState to be updated</param>
        /// <returns>Updated ProductState</returns>
        ProductState UpdateProductState(ProductState productState);

        /// <summary>
        /// Deletes a ProductState with the specified id
        /// </summary>
        /// <param name="id">ProductState's id</param>
        void DeleteProductState(int id);
    }
}
