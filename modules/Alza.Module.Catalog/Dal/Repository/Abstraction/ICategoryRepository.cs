using Alza.Core.Module.Abstraction;
using Alza.Module.Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Repository.Abstraction
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByName(string name);
        Category GetBySEOName(string seoName);


        int GetStats1ByCategoryName(string categoryName = null, string MediaType = null);

        Dictionary<string, int> GetStats1(string MediaType);
    }
}
