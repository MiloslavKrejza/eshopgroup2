using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainee.Business.Business.Enums
{
    public enum SortingParameter
    {
        [Display(Name = "Od nejnovější")]
        Date,
        [Display(Name = "Ceny")]
        Price,
        [Display(Name = "Hodnocení")]
        Rating,
        [Display(Name = "Názvu")]
        Name
    }
}
