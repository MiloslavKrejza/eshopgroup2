using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    public class CartItem
    {
        public int? UserId { get; set; }
        public int VisitorId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
