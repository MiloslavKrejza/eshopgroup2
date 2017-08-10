using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Catalogue.Abstraction;
using Alza.Core.Module.Http;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.Business
{
    public class CatalogueService
    {
        private readonly ICategoryRepository _catRepos; 

        public CatalogueService(ICategoryRepository catRepos)
        {
            _catRepos = catRepos;
        }

        /// <summary>
        /// Gets category with the specified id with its child categories (only sons) included
        /// </summary>
        /// <param name="id">Category unique identifier</param>
        /// <returns>DTO of Category with Children included</returns>
        public AlzaAdminDTO<Category> GetCategory(int id)
        {
            var result = _catRepos.GetCategory(id);
            return AlzaAdminDTO<Category>.Data(result);
        }
    }
}
