using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IProductRatingRepository
    {
        ProductRating GetRating(int id);
        IQueryable<ProductRating> GetRatings();

    }
}
