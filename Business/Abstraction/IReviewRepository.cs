using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IReviewRepository
    {
        Review GetReview(int userId, int productId);
        IQueryable<Review> GetReviews();
        Review AddReview(Review review);
        Review UpdateReview(Review reviews);
        void DeleteReview(int userId, int productId);
    }
}
