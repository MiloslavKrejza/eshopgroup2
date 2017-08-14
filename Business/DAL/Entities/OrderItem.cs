using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
