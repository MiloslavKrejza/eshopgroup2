using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    class ReviewRepository : IReviewRepository

    {
        BusinessDbContext _context;
        public ReviewRepository(BusinessDbContext context)
        {
            _context = context;
        }
        public Review AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public void DeleteReview(int userId, int productId)
        {
            Review review = _context.Reviews.Where(r => r.ProductId == productId && r.UserId == userId).FirstOrDefault();
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public Review GetReview(int userId, int productId)
        {
            return _context.Reviews.Where(r => r.ProductId == productId && r.UserId == userId).FirstOrDefault();
        }

        public IQueryable<Review> GetReviews()
        {
            return _context.Reviews.AsQueryable();
        }

        public Review UpdateReview(Review review)
        {
            Review currentReview = _context.Reviews.Where(r => r.ProductId == review.ProductId && r.UserId == review.UserId).FirstOrDefault();
            _context.Entry(currentReview).CurrentValues.SetValues(review);
            _context.SaveChanges();
            return review;
        }
    }
}
