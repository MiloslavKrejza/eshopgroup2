using System;
using System.Collections.Generic;
using System.Text;
using Trainee.User.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    public class Review
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }       
        //Referenced properties
        //public UserWithReviews User { get; set; }
        //public ProductBO Product { get; set; }

    }
}
