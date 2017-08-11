using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    /// <summary>
    /// This class represents a relationship parent-son of categories
    /// </summary>
    public class CategoryRelationshipBO
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
    }
}
