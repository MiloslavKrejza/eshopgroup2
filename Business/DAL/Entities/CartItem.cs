using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Trainee.User.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    public class CartItem
    {
        public int? UserId { get; set; }
        public string VisitorId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        [NotMapped]
        public UserProfile User { get; set; }
        [NotMapped]
        public ProductBO Product { get; set; }
    }
}
