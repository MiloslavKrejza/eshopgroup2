using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface ICategoryRelationshipRepository
    {
        /// <summary>
        /// Gets an IQueryable object of parent-child category relationships
        /// </summary>
        /// <returns>IQueryable of relationships</returns>
        IQueryable<CategoryRelationshipBO> GetAllRelationships();
       
    }
}
