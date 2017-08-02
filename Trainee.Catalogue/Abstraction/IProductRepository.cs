using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Abstraction
{
    public interface IProductRepository
    {
        /// <summary>
        /// Adds a new Product to the database
        /// </summary>
        /// <param name="product">Product to be added</param>
        /// <returns>Added Product</returns>
        Product AddProduct(Product product);

        /// <summary>
        /// Gets a Product with the specified id
        /// </summary>
        /// <param name="id">Id of the Product to be get</param>
        /// <returns>Boook with the specified id</returns>
        Product GetProduct(int id);

        /// <summary>
        /// Gets an IQueryable of all Products
        /// </summary>
        /// <returns>IQueryable of Product</returns>
        IQueryable<Product> GetAllProducts();

        /// <summary>
        /// Updates a Product in the database
        /// </summary>
        /// <param name="product">Product to be updated</param>
        /// <returns>Updated Product</returns>
        Product UpdateProduct(Product product);

        /// <summary>
        /// Deletes a Product with the specified id
        /// </summary>
        /// <param name="id">Product's id</param>
        void DeleteProduct(int id);
    }
}
