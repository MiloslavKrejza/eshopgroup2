using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainee.Business.Business.Enums
{
    /// <summary>
    /// This enum specifies how to sort the products (ascending or descending)
    /// </summary>
    public enum SortType
    {
        [Display(Name = "Vzestupně")]
        Asc,
        [Display(Name = "Sestupně")]
        Desc
    }
}
