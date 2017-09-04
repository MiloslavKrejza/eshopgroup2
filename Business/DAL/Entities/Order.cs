using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Trainee.Core.DAL.Entities;
using Trainee.User.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int StateId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public DateTime? Date { get; set; }

        //Referenced properties
        public Country Country { get; set; }

        //Referenced
        public OrderState OrderState { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
