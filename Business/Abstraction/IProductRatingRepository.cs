using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IProductRatingRepository
    {
        /// <summary>
        /// Gets average rating of a product
        /// </summary>
        /// <param name="id">ProductId</param>
        /// <returns>Average rating</returns>
        ProductRating GetRating(int id);
        /// <summary>
        /// Gets ratings for all products
        /// </summary>
        /// <returns>All ratings</returns>
        IQueryable<ProductRating> GetRatings();

    }
}
