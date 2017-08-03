using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    public class ProductWithReviewsBO : RatedProductBO
    {
        public List<Review> Reviews { get; set; }
    }
}
