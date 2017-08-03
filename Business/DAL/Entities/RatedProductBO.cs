using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    /// <summary>
    /// Business entity made using a database view.
    /// </summary>
    public class RatedProductBO : Product
    {
        public decimal? AverageRating { get; set; }
    }
}
