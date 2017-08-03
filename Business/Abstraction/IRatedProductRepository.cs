using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IRatedProductRepository
    {
        IQueryable<RatedProductBO> GetAllRatedProducts();
        RatedProductBO GetRatedProduct(int id);
        
    }
}
