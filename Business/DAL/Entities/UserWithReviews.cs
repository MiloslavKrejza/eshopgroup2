using System;
using System.Collections.Generic;
using System.Text;
using Trainee.User.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    public class UserWithReviews : UserProfile
    {
        public List<Review> Reviews { get; set; }
    }
}
