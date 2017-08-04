using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{
    class ProductRatingRepository : IProductRatingRepository
    {
        BusinessDbContext _context;
        public ProductRatingRepository(BusinessDbContext context)
        {
            _context = context;
        }
        public ProductRating GetRating(int id)
        {
            return _context.ProductRatings.Where(pr => pr.ProductId == id).SingleOrDefault();
        }

        public IQueryable<ProductRating> GetRatings()
        {
            return _context.ProductRatings.AsQueryable();
        }
    }
}
