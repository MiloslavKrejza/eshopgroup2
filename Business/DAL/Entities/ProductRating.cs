using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    /// <summary>
    /// This class specifies the average rating of a product
    /// </summary>
    public class ProductRating
    {
        public int ProductId { get; set; }
        public decimal? AverageRating { get; set; }
    }
}
