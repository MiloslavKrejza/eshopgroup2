using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IReviewRepository
    {
        /// <summary>
        /// Gets user review of a product
        /// </summary>
        /// <returns>User's review of a product</returns>
        Review GetReview(int userId, int productId);
        /// <summary>
        /// Gets all reviews
        /// </summary>
        /// <returns>IQueryable of review</returns>
        IQueryable<Review> GetReviews();
        /// <summary>
        /// Adds a new review
        /// </summary>
        /// <param name="userId">User who submitted the review</param>
        /// <param name="productId">Rated product</param>
        /// <returns>Added Review</returns> 
        Review AddReview(Review review);
        /// <summary>
        /// Updates a review with new data
        /// </summary>
        /// <param name="review">Review data to update</param>
        /// <returns>Updated review</returns>
        Review UpdateReview(Review review);
        /// <summary>
        /// Deletes a review with specified UserId and ProductId
        /// </summary>
        /// <param name="userId">User's identifier</param>
        /// <param name="productId">Product identifier</param>
        void DeleteReview(int userId, int productId);
    }
}
