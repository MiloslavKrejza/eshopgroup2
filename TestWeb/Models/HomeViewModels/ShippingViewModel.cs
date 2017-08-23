using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.DAL.Entities;

namespace Eshop2.Models.HomeViewModels
{
    public class ShippingViewModel
    {
        public List<Shipping> Shipping { get; set; }
        public List<Payment> Payment { get; set; }
    }
}
