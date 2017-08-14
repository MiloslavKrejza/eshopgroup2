using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    class Cart
    {
        public int? UserId { get; set; }
        public int VisitorId { get; set; }
        public int ProductId { get; set; }
    }
}
