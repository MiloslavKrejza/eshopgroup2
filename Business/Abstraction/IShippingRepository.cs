using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    interface IShippingRepository
    {
        /// <summary>
        /// Gets Shipping type
        /// </summary>
        /// <returns>Shipping type</returns>
        Shipping GetShipping(int shippingId);

        /// <summary>
        /// Gets all Shipping types
        /// </summary>
        /// <returns>IQueryable of Shipping</returns>
        IQueryable<Shipping> GetShippings();

        /// <summary>
        /// Adds a new Shipping
        /// </summary>
        /// <param name="shippingId">New Shipping type</param>
        /// <returns>Added Shipping</returns> 
        Shipping AddShipping(Shipping shipping);

        /// <summary>
        /// Updates a Shipping with new data
        /// </summary>
        /// <param name="shipping">Shipping data to update</param>
        /// <returns>Updated Shipping</returns>
        Shipping UpdateShipping(Shipping shipping);

        /// <summary>
        /// Deletes a Shipping with specified ShippingId
        /// </summary>
        /// <param name="shippingId">Shipping identifier</param>
        void DeleteShipping(int shippingId);
    }
}
