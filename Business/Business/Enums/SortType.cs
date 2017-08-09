using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainee.Business.Business.Enums
{
    public enum SortType
    {
        [Display(Name = "Vzestupně")]
        Asc,
        [Display(Name = "Sestupně")]
        Desc
    }
}
