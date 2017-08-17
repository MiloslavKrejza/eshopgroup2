using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.DAL.Entities;

namespace Eshop2.Models.OrderViewModels
{
    public class CartViewModel
    {
        public List<CartItem> Cart { get; set; }

    }
}
