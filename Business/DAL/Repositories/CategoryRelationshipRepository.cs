using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Context;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.DAL.Repositories
{

    public class CategoryRelationshipRepository : ICategoryRelationshipRepository
    {
        BusinessDbContext _context;
        public CategoryRelationshipRepository(BusinessDbContext context)
        {
            _context = context;
        }
        public IQueryable<CategoryRelationshipBO> GetAllRelationships()
        {
            return _context.CategoryRelationships.AsQueryable();
        }
    }
}
