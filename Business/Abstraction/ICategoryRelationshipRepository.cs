using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface ICategoryRelationshipRepository
    {
        ICollection<CategoryRelationshipBO> GetRelationships();
       
    }
}
